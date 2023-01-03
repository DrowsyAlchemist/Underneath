using UnityEngine;

public class VioletMissile : Missile
{
    [SerializeField] private AudioSource _launchSound;
    [SerializeField] private float _explosionRadius;

    public override void Launch(Vector2 direction)
    {
        _launchSound.Play();
        base.Launch(direction);
    }

    protected override void Hit(Collider2D collision)
    {
        RaycastHit2D[] hits = new RaycastHit2D[8];
        int hitsCount = Physics2D.CircleCast(transform.position, _explosionRadius, Vector2.zero, Filter, hits, 0);

        for (int i = 0; i < hitsCount; i++)
            if (hits[i].transform.TryGetComponent(out VioletWraith _) == false)
                if (hits[i].transform.TryGetComponent(out ITakeDamage target))
                    target.TakeDamage(Damage, transform.position);

        Collapse();
    }
}
