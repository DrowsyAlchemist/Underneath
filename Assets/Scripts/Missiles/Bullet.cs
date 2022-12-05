using UnityEngine;

public class Bullet : Missile
{
    public override void Launch(Vector2 direction)
    {
        transform.LookForwardDirection(direction);
        base.Launch(direction);
    }
}