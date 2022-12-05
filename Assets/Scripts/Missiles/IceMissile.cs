using UnityEngine;

public class IceMissile : Missile
{
    protected override void Hit(Collider2D collision)
    {
        if (collision.transform.TryGetComponent(out ITakeDamage target))
            target.TakeDamage(Damage, transform.position);
        else
            Collapse();
    }
}