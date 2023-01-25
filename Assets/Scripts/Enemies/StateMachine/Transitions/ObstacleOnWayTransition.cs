using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class ObstacleOnWayTransition : EnemyTransition
{
    [SerializeField] private LayerMask _obstacles;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if ((1 << collision.gameObject.layer & _obstacles) > 0)
            NeedTransit = true;
    }

    private void OnValidate()
    {
        GetComponent<Collider2D>().isTrigger = true;
        GetComponent<Rigidbody2D>().isKinematic = true;
    }
}