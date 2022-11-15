using UnityEngine;

public class Life : Collectable
{
    [SerializeField] private int _lifesToIncrease = 1;

    protected override void CollectByPlayer(Player player)
    {
        player.PlayerHealth.IncreaseMaxHealth(_lifesToIncrease);
    }
}
