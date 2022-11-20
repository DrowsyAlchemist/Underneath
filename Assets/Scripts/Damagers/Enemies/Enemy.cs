using UnityEngine;

[RequireComponent(typeof(EnemyMovement))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private Player _target;

    private EnemyMovement _movement;

    public Player Target => _target;
    public EnemyMovement Movement => _movement;

    private void Awake()
    {
        _movement = GetComponent<EnemyMovement>();
    }

}
