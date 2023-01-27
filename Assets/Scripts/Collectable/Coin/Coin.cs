using UnityEngine;

[RequireComponent(typeof(CollectableMovement))]
public class Coin : Collectable
{
    private const int CoinWorth = 1;

    public CollectableMovement Movement { get; private set;}

    private void Awake()
    {
        Movement = GetComponent<CollectableMovement>();
    }

    protected override void CollectByPlayer(Player player)
    {
        Movement.MoveToCollector(player);
        player.Wallet.TakeMoney(CoinWorth);
    }
}