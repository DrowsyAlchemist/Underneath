using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Coin : Collectable
{
    [SerializeField] private float _collectedSpeed;

    public void AddForce(Vector2 force)
    {
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.isKinematic = false;
        rigidbody.AddForce(force, ForceMode2D.Impulse);
    }

    protected override void CollectByPlayer(Player player)
    {
        StartCoroutine(MoveToPlayer(player));
        player.TakeMoney(1);
    }

    private IEnumerator MoveToPlayer(Player player)
    {
        float elapsedTime = 0;

        while (gameObject)
        {
            elapsedTime += Time.deltaTime * _collectedSpeed;
            transform.position = Vector2.Lerp(transform.position, player.GetWorldCenter(), elapsedTime);
            yield return null;
        }
    }
}
