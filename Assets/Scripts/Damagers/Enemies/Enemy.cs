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

    private const int DefaultDamage = 1;
    private bool _isHurting;

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
            player.TakeDamage(DefaultDamage);
    }

    public void TakeDamage(int damage, GameObject sourse)
    {
        if (damage < 0)
            throw new System.ArgumentOutOfRangeException();

        if (_isHurting == false)
        {
            _isHurting = true;
            StateMachine.Pause();
            _health -= damage;

            if (_health > 0)
                StartCoroutine(Hurt());
            else
                StartCoroutine(Die());
        }
    }

    private IEnumerator Hurt()
    {
        EnemyAnimator.PlayHurt();
        yield return new WaitForEndOfFrame();
        yield return new WaitForSeconds(EnemyAnimator.Animator.GetCurrentAnimatorStateInfo(0).length);
        StateMachine.Resume();
        _isHurting = false;
    }

    private IEnumerator Die()
    {
        EnemyAnimator.PlayDie();
        yield return new WaitForEndOfFrame();
        yield return new WaitForSeconds(EnemyAnimator.Animator.GetCurrentAnimatorStateInfo(0).length);
        Destroy(gameObject);
    }

    private void OnValidate()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }
}
