using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(EnemyMovement))]
[RequireComponent(typeof(EnemyStateMachine))]
[RequireComponent(typeof(EnemyAnimator))]
public class Enemy : MonoBehaviour, ITakeDamage
{
    [SerializeField] private int _health;
    [SerializeField] private EnemyData _enemyData;
    [SerializeField] private EnemySounds _enemySounds;

    private bool _isAlive = true;
    private Coroutine _hurtCoroutine;

    public Player Target => AccessPoint.Player;
    public EnemyAnimator EnemyAnimator { get; private set; }
    public EnemyMovement Movement { get; private set; }
    protected EnemyStateMachine StateMachine { get; private set; }

    private void Awake()
    {
        EnemyAnimator = GetComponent<EnemyAnimator>();
        Movement = GetComponent<EnemyMovement>();
        StateMachine = GetComponent<EnemyStateMachine>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Spikes _))
            if (_isAlive)
                StartCoroutine(Die());
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
            if (_isAlive)
                player.TakeDamage(_enemyData.DefaultDamage, transform.position);
    }

    public void TakeDamage(int damage, Vector3 attackerPosition)
    {
        if (damage < 0)
            throw new System.ArgumentOutOfRangeException();

        StateMachine.Pause();
        Instantiate(_enemyData.HitEffect, transform.position, Quaternion.identity, null);
        transform.LookForwardDirection(attackerPosition - transform.position);
        _health -= damage;

        if (_hurtCoroutine != null)
            StopCoroutine(_hurtCoroutine);

        if (_health > 0)
            _hurtCoroutine = StartCoroutine(Hurt(attackerPosition));
        else
            StartCoroutine(Die());
    }

    private IEnumerator Hurt(Vector3 attackerPosition)
    {
        _enemySounds.PlayHurt();
        EnemyAnimator.PlayIdle();
        yield return new WaitForEndOfFrame();
        EnemyAnimator.PlayHurt();
        yield return new WaitForEndOfFrame();
        float hurtDuration = EnemyAnimator.Animator.GetCurrentAnimatorStateInfo(0).length;
        Movement.FlyAway(attackerPosition, hurtDuration);
        yield return new WaitForSeconds(hurtDuration);
        StateMachine.Resume();
    }

    private IEnumerator Die()
    {
        _enemySounds.PlayDeath();
        _isAlive = false;
        EnemyAnimator.PlayDie();
        yield return new WaitForEndOfFrame();
        yield return new WaitForSeconds(EnemyAnimator.Animator.GetCurrentAnimatorStateInfo(0).length);
        CoinsSpawner.Spawn(_enemyData.Award, transform.position, useModifier: true);

        if (gameObject)
            Destroy(gameObject);
    }

    private void OnValidate()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }
}