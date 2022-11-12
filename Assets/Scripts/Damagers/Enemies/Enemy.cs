using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(EnemyAnimation))]
public abstract class Enemy : Damager
{
    [SerializeField] protected EnemyPatrol Patrol;

    protected Collider2D Collider { get; private set; }
    protected EnemyAnimation EnemyAnimation { get; private set; }

    private void Awake()
    {
        Collider = GetComponent<Collider2D>();
        EnemyAnimation = GetComponent<EnemyAnimation>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
            Attack(player);
    }

    protected virtual void Attack(Player player)
    {
        if (Patrol)
            Patrol.StopPatrol();

        transform.TurnToTarget(player.transform);
        EnemyAnimation.PlayAttack();
    }
}
