using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private LayerMask _obstacles;
    [SerializeField] private float _flyAwayDistance = 1;

    private Collider2D _collider;
    private ContactFilter2D _contactFilter = new ContactFilter2D();
    private Coroutine _coroutine;

    public ContactFilter2D Obstacles => _contactFilter;

    private void Start()
    {
        _collider = GetComponent<Collider2D>();
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
        int hitsCount = _collider.Cast(direction, _contactFilter, results, step);

        if (hitsCount > 0)
        {
            direction = GetSurfaseAlong(results[0]);
            hitsCount = _collider.Cast(direction, _contactFilter, results, step);

            if (hitsCount > 0)
                direction = results[0].normal;
        }
        return direction;
    }

    public void FlyAway(Vector3 soursePosition, float duration)
    {
        int direction = (transform.position.x - soursePosition.x > 0) ? 1 : -1;

        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(FlyAway(direction, duration));
    }

    private IEnumerator FlyAway(int direction, float duration)
    {
        float targetPositionX = transform.position.x + direction * _flyAwayDistance;
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float newXposition = Mathf.Lerp(transform.position.x, targetPositionX, elapsedTime / duration);
            Vector2 step = (newXposition - transform.position.x) * Vector2.right;

            if (IsObstacleOnWay(step))
                break;
            else
                transform.Translate(step);

            yield return null;
        }
    }

    private bool IsObstacleOnWay(Vector2 step)
    {
        int hitsCount = _collider.Cast(step, _contactFilter, new RaycastHit2D[1], step.magnitude);
        return hitsCount > 0;
    }

    private Vector2 GetSurfaseAlong(RaycastHit2D surfase)
    {
        return new Vector2(surfase.normal.y, -1 * surfase.normal.x);
    }
}
