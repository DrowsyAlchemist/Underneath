using UnityEngine;

[RequireComponent(typeof(EnemyAnimator))]
[RequireComponent(typeof(Enemy))]
public abstract class EnemyState : MonoBehaviour
{
    [SerializeField] private EnemyTransition[] _transitions;

    protected Transform Target;

    protected EnemyAnimator EnemyAnimation { get; private set; }

    private void Awake()
    {
        enabled = false;
        EnemyAnimation = GetComponent<EnemyAnimator>();
        Target = GetComponent<Enemy>().Target.transform;
    }

    public void Enter()
    {
        enabled = true;

        foreach (var transition in _transitions)
            transition.enabled = true;
    }

    public void Exit()
    {
        foreach (var transition in _transitions)
            transition.enabled = false;

        enabled = false;
    }

    public EnemyState GetNextState()
    {
        foreach (var transition in _transitions)
            if (transition.NeedTransit)
                return transition.TargetState;

        return null;
    }
}
