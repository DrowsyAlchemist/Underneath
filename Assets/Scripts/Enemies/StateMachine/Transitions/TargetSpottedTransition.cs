using UnityEditor;
using UnityEngine;

public class TargetSpottedTransition : EnemyTransition
{
    [SerializeField] private float _radius = 6;
    [SerializeField] private bool _canFly;
    [SerializeField] private bool _canSeeThrowObstacles;
    [SerializeField] private float _maxTargetHeight = 5;
    [SerializeField] private float _minTargetHeight = -2;

    private void FixedUpdate()
    {
        {   // Debug
            Vector2 dir = (Target.GetWorldCenter() - transform.position).normalized * _radius;//
            Debug.DrawRay(transform.position, dir);//
        }

        if (_canFly == false)
            if (IsVerticalInvalid())
                return;

        NeedTransit = IsTargetInDeadZone();
    }

    private bool IsVerticalInvalid()
    {
        if (Target.GetWorldCenter().y - transform.position.y > _maxTargetHeight)
            return true;

        if (Target.GetWorldCenter().y - transform.position.y < _minTargetHeight)
            return true;

        return false;
    }

    private bool IsTargetInDeadZone()
    {
        var direction = Target.GetWorldCenter() - transform.position;

        if (direction.magnitude < _radius)
        {
            if (_canSeeThrowObstacles)
            {
                return true;
            }
            else
            {
                var origin = transform.position;
                var contactFilter = Enemy.Movement.Obstacles;
                var hits = new RaycastHit2D[1];
                var distance = direction.magnitude;
                return Physics2D.Raycast(origin, direction, contactFilter, hits, distance) == 0;
            }
        }
        return false;
    }
}
