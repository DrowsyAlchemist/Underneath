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
    [SerializeField] private LayerMask _ground;

    private const int DefaultDamage = 1;
    private bool _isHurting;
    private Coroutine _flyAwayCoroutine;

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
        {
            player.TakeDamage(DefaultDamage, transform.position);
        }
        else if ((1 << collision.gameObject.layer & _ground) > 0)
        {
            if (_flyAwayCoroutine != null)
                StopCoroutine(_flyAwayCoroutine);
        }
    }

    public void TakeDamage(int damage, Vector3 soursePosition)
    {
        if (damage < 0)
            throw new System.ArgumentOutOfRangeException();

        if (_isHurting == false)
        {
            _isHurting = true;
            StateMachine.Pause();
            _health -= damage;
            _flyAwayCoroutine = StartCoroutine(FlyAway(soursePosition));

            if (_health > 0)
                StartCoroutine(Hurt());
            else
                StartCoroutine(Die());
        }
    }

    private IEnumerator FlyAway(Vector3 soursePosition)
    {
        int direction = (transform.position.x - soursePosition.x > 0) ? 1 : -1;
        float counter = 0;

        while (counter < 0.3f)
        {
            transform.Translate(6 * Vector2.right * direction * Time.deltaTime);
            counter += Time.deltaTime;
            yield return null;
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

        if (gameObject)
            Destroy(gameObject);
    }

    private void OnValidate()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }
}
