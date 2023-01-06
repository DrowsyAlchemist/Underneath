using UnityEngine;

public class IceMissile : Missile
{
    public override void Launch(Vector2 direction)
    {
        direction = direction.x * Vector2.right;
        transform.LookForwardDirection(direction);
        base.Launch(direction);
    }

    protected override void Hit(Collider2D collision)
    {
        if (collision.transform.TryGetComponent(out ITakeDamage target))
            target.TakeDamage(Damage, transform.position);
        else
            Collapse();
    }
}