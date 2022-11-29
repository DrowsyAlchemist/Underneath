using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(EnemyMovement))]
[RequireComponent(typeof(EnemyStateMachine))]
[RequireComponent(typeof(EnemyAnimator))]
public class Enemy : MonoBehaviour, ITakeDamage
{
    [SerializeField] private Game _game;
    [SerializeField] private int _health;
    [SerializeField] int _defaultDamage = 1;

    private Coroutine _hurtCoroutine;

    public Player Target => _game.Player;
    public EnemyAnimator EnemyAnimator { get; private set; }
    public EnemyMovement Movement { get; private set; }
    protected EnemyStateMachine StateMachine { get; private set; }

    private void Awake()
    {
        EnemyAnimator = GetComponent<EnemyAnimator>();
        Movement = GetComponent<EnemyMovement>();
        StateMachine = GetComponent<EnemyStateMachine>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
            player.TakeDamage(_defaultDamage, transform.position);
    }

    public void TakeDamage(int damage, Vector3 attackerPosition)
    {
        if (damage < 0)
            throw new System.ArgumentOutOfRangeException();

        StateMachine.Pause();
        transform.LookForwardDirection(attackerPosition - transform.position);
        _health -= damage;

        if (_hurtCoroutine != null)
            StopCoroutine(_hurtCoroutine);

        if (_health > 0)
        {
            _hurtCoroutine = StartCoroutine(Hurt(attackerPosition));
        }
        else
        {
            StartCoroutine(Die());
        }
    }

    private IEnumerator Hurt(Vector3 attackerPosition)
    {
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
        EnemyAnimator.PlayDie();
        yield return new WaitForEndOfFrame();
        yield return new WaitForSeconds(EnemyAnimator.Animator.GetCurrentAnimatorStateInfo(0).length);

        if (gameObject)
            Destroy(gameObject);
    }

    private void OnValidate()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }
}
