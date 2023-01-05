using UnityEngine;

public class FallingTransition : EnemyTransition
{
    [SerializeField] private GroundChecker _groundChecker;

    private void Update()
    {
        if (_groundChecker.IsGrounded == false)
            NeedTransit = true;
    }
}
