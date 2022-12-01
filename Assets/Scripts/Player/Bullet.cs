using UnityEngine;

public class Bullet : Missile
{
    public override void Launch(Vector2 direction)
    {
        transform.LookForwardDirection(-1 * direction);
        base.Launch(direction);
    }
}