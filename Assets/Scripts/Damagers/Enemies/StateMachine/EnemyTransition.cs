using UnityEngine;

[RequireComponent(typeof(Enemy))]
public abstract class EnemyTransition : MonoBehaviour
{
    [SerializeField] private EnemyState _targetState;

    public EnemyState TargetState => _targetState;
    public bool NeedTransit { get; protected set; }

    protected Enemy Enemy { get; private set; }
    protected Player Target { get; private set; }


    private void Awake()
    {
        enabled = false;
        Enemy = GetComponent<Enemy>();
        Target = GetComponent<Enemy>().Target;
    }

    private void OnEnable()
    {
        NeedTransit = false;
    }
}
