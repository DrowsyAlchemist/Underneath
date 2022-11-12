using UnityEngine;

[RequireComponent(typeof(Enemy))]
public abstract class EnemyTransition : MonoBehaviour
{
    [SerializeField] private EnemyState _targetState;

    protected Transform Target;

    public EnemyState TargetState => _targetState;
    public bool NeedTransit { get; protected set; }

    private void Awake()
    {
        enabled = false;
        Target = GetComponent<Enemy>().Target.transform;
    }

    private void OnEnable()
    {
        NeedTransit = false;
    }
}
