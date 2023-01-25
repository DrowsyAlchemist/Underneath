using UnityEngine;

public class TargetLostTransition : EnemyTransition
{
    [SerializeField] private float _maxTargetDistance = 5;
    [SerializeField] private bool _canFly;

    [SerializeField] private float _maxTargetHeight = 4;
    [SerializeField] private float _minTargetHeight = -3;

    private void FixedUpdate()
    {
        if ((Vector2.Distance(transform.position, Target.GetPosition()) > _maxTargetDistance))
            NeedTransit = true;

        if (_canFly == false)
            CheckVertical();
    }

    private void CheckVertical()
    {
        if (Target.GetPosition().y - transform.position.y > _maxTargetHeight)
            NeedTransit = true;

        if (Target.GetPosition().y - transform.position.y < _minTargetHeight)
            NeedTransit = true;
    }
}