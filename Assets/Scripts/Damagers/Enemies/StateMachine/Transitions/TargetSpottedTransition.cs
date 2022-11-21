using UnityEngine;

public class TargetSpottedTransition : EnemyTransition
{
    [SerializeField] private float _radius;

    private void Update()
    {
        Vector2 direction = Target.GetWorldCenter() - transform.position;
        ContactFilter2D contactFilter = Enemy.Movement.Obstacles;

        if (direction.magnitude < _radius)
            if (Physics2D.Raycast(transform.position, direction, contactFilter, new RaycastHit2D[1], direction.magnitude) == 0)
                NeedTransit = true;
    }
}
