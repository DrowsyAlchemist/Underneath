using UnityEngine;

public class Icicle : Missile
{
    public override void Launch(Vector2 direction)
    {
        float angle = Vector2.Angle(transform.right, direction);
        transform.rotation = Quaternion.Euler(0, 0, angle);
        base.Launch(direction);
    }
}
