using UnityEngine;

public class TargetSpottedTransition : EnemyTransition
{
    [SerializeField] private float _radius;
    [SerializeField] private LayerMask _obstacles;

    private ContactFilter2D _contactFilter = new ContactFilter2D();

    private void Start()
    {
        _contactFilter.useLayerMask = true;
        _contactFilter.layerMask = _obstacles;
    }

    private void Update()
    {
        float distanceToTarget = Vector2.Distance(Target.GetWorldCenter(), transform.position);

        if (distanceToTarget < _radius)
        {
            Vector2 direction = transform.position - Target.GetWorldCenter();
            int hitsCount = Physics2D.Raycast(transform.position, direction, _contactFilter, new RaycastHit2D[1], distanceToTarget);

            if (hitsCount == 0)
                NeedTransit = true;
        }
    }
}
