using UnityEngine;

public class Bullet : Missile
{
    protected override void RotateMissile(Vector2 direction)
    {
        transform.LookForwardDirection(direction);
    }
}