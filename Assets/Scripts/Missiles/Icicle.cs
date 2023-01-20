using UnityEngine;

public class Icicle : Missile
{
    protected override void RotateMissile(Vector2 direction)
    {
        float angle = Vector2.Angle(transform.right, direction);
        transform.right = Quaternion.Euler(0, 0, angle) * transform.right;
    }
}
