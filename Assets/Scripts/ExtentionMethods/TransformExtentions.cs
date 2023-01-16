using UnityEngine;

public static class TransformExtentions
{
    public static void LookForwardDirection(this Transform transform, Vector2 direction)
    {
        if (transform.localScale.x * direction.x < 0)
            transform.localScale = new Vector3(-1 * transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    public static void TurnToTarget(this Transform transform, Transform target)
    {
        LookForwardDirection(transform, target.position - transform.position);
    }
}