using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ÑrossedWithPlayerTransotion : EnemyTransition
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
            NeedTransit = true;
    }

    private void OnValidate()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }
}
