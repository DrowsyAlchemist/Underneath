using UnityEngine;

[RequireComponent(typeof(EnemyMovement), typeof(Collider2D))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private Player _target;

    private const int DefaultDamage = 1;

    public Player Target => _target;
    public EnemyMovement Movement { get; private set; }

    private void Awake()
    {
        Movement = GetComponent<EnemyMovement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
            player.TakeDamage(DefaultDamage);
    }

    private void OnValidate()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }
}
