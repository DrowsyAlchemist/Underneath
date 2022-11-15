using UnityEngine;

public class Coin : Collectable
{
    [SerializeField] private int _money = 1;
    protected override void CollectByPlayer(Player player)
    {
        player.TakeMoney(_money);
    }
}
