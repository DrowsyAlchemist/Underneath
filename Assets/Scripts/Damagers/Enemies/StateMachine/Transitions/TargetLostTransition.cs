using UnityEngine;

public class TargetLostTransition : EnemyTransition
{
    [SerializeField] private float _maxTargetDistance = 5;

    private void FixedUpdate()
    {
        if (Vector2.Distance(transform.position, Target.GetWorldCenter()) > _maxTargetDistance)
            NeedTransit = true;
    }
}
