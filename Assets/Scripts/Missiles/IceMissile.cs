using UnityEngine;

public class IceMissile : Missile
{
    protected override void RotateMissile(Vector2 direction)
    {
        direction = direction.x * Vector2.right;
        transform.LookForwardDirection(direction);
    }
}