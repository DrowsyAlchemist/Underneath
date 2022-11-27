using UnityEngine;

public class Bullet : Missile
{
    public override void Launch(Vector2 direction)
    {
        bool positiveDirection = transform.localScale.x > 0;
        transform.LookForwardDirection(positiveDirection);
        base.Launch(direction);
    }
}