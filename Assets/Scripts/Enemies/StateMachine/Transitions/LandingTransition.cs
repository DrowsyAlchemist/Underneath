using UnityEngine;

public class LandingTransition : EnemyTransition
{
    [SerializeField] private GroundChecker _groundChecker;

    private void FixedUpdate()
    {
        if (_groundChecker.IsGrounded)
            NeedTransit = true;
    }
}