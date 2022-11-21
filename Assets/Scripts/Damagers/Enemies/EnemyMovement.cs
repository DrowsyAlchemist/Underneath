using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private LayerMask _obstacles;

    private Rigidbody2D _rigidBody;
    private ContactFilter2D _contactFilter = new ContactFilter2D();

    public ContactFilter2D Obstacles => _contactFilter;

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _contactFilter.useLayerMask = true;
        _contactFilter.layerMask = _obstacles;
    }

    public void MoveToTarget(Vector3 targetPosition, float speed)
    {
        Move(targetPosition, speed, positiveDirection: true, xAxisOnly: false);
    }
    public void MoveToTargetAlongXAxis(Vector3 targetPosition, float speed)
    {
        Move(targetPosition, speed, positiveDirection: true, xAxisOnly: true);
    }

    public void MoveFromTarget(Vector3 targetPosition, float speed)
    {
        Move(targetPosition, speed, positiveDirection: false, xAxisOnly: false);
    }

    public void MoveFromTargetAlongXAxis(Vector3 targetPosition, float speed)
    {
        Move(targetPosition, speed, positiveDirection: false, xAxisOnly: true);
    }

    private void Move(Vector3 targetPosition, float speed, bool positiveDirection = true, bool xAxisOnly = false)
    {
        Vector2 targetLocalPosition = targetPosition - transform.position;
        Vector2 direction = (positiveDirection ? 1 : -1) * targetLocalPosition.normalized;
        float step = speed * Time.deltaTime;

        if (xAxisOnly)
            direction = (direction.x > 0 ? 1 : -1) * Vector2.right;
        else
            direction = CalculateDirectionInAir(direction, step);

        transform.Translate(step * direction);
    }

    private Vector2 CalculateDirectionInAir(Vector2 direction, float step)
    {
        RaycastHit2D[] results = new RaycastHit2D[1];
        int hitsCount = _rigidBody.Cast(direction, _contactFilter, results, step);

        if (hitsCount > 0)
        {
            direction = GetSurfaseAlong(results[0]);
            hitsCount = _rigidBody.Cast(direction, _contactFilter, results, step);

            if (hitsCount > 0)
                direction = results[0].normal;
        }
        return direction;
    }

    private Vector2 GetSurfaseAlong(RaycastHit2D surfase)
    {
        return new Vector2(surfase.normal.y, -1 * surfase.normal.x);
    }
}
