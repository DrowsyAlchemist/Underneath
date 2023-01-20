using UnityEngine;

public class VioletMissile : Missile
{
    [SerializeField] private float _explosionRadius;

    protected override void DoDamage(Collider2D collision, int damage)
    {
        RaycastHit2D[] hits = new RaycastHit2D[8];
        int hitsCount = Physics2D.CircleCast(transform.position, _explosionRadius, Vector2.zero, ContactFilter, hits, 0);

        for (int i = 0; i < hitsCount; i++)
            if (hits[i].transform.TryGetComponent(out VioletWraith _) == false)
                if (hits[i].transform.TryGetComponent(out ITakeDamage target))
                    target.TakeDamage(damage, transform.position);
    }
}