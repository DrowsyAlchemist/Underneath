using UnityEngine;

[RequireComponent(typeof(EnemyAnimation))]
public abstract class State : MonoBehaviour
{
    [SerializeField] private Transition[] _transitions;

    protected EnemyAnimation EnemyAnimation { get; private set; }

    private void Awake()
    {
        enabled = false;
        EnemyAnimation = GetComponent<EnemyAnimation>();
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

    public State GetNextState()
    {
        foreach (var transition in _transitions)
            if (transition.NeedTransit)
                return transition.TargetState;

        return null;
    }
}
