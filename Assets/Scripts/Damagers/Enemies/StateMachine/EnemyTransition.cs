using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public abstract class EnemyTransition : MonoBehaviour
{
    [SerializeField] private EnemyState _targetState;

    protected Player Target;

    public EnemyState TargetState => _targetState;
    public bool NeedTransit { get; protected set; }

    private void Awake()
    {
        enabled = false;
        Target = GetComponent<Enemy>().Target;
    }

    private void OnEnable()
    {
        NeedTransit = false;
    }
}
