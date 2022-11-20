using UnityEngine;

[RequireComponent(typeof(Enemy))]
public abstract class EnemyTransition : MonoBehaviour
{
    [SerializeField] private EnemyState _targetState;

    private Enemy _enemy;
    private Player _target;

    public EnemyState TargetState => _targetState;
    public bool NeedTransit { get; protected set; }

    protected Enemy Enemy => _enemy;
    protected Player Target => _target;


    private void Awake()
    {
        enabled = false;
        _enemy = GetComponent<Enemy>();
        _target = GetComponent<Enemy>().Target;
    }

    private void OnEnable()
    {
        NeedTransit = false;
    }
}
