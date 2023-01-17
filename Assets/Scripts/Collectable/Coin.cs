using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Coin : Collectable
{
    [SerializeField] private float _collectionDuration = 0.33f;

    private const int CoinWorth = 1;
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void AddForce(Vector2 force)
    {
        _rigidbody.AddForce(force, ForceMode2D.Impulse);
    }

    protected override void CollectByPlayer(Player player)
    {
        StartCoroutine(MoveToPlayer(player));
        player.Wallet.TakeMoney(CoinWorth);
    }

    private IEnumerator MoveToPlayer(Player player)
    {
        float elapsedTime = 0;
        float t;

        while (gameObject)
        {
            elapsedTime += Time.deltaTime;
            t = elapsedTime / _collectionDuration;
            transform.position = Vector2.Lerp(transform.position, player.GetPosition(), t);
            yield return null;
        }
    }
}