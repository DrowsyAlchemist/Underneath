using UnityEngine;

public class LandingTransition : EnemyTransition
{
    [SerializeField] private GroundChecker _groundChecker;

    private void Update()
    {
        if (_groundChecker.IsGrounded)
            NeedTransit = true;
    }
}
