using UnityEngine;

public class HealingPotion : Potion
{
    [SerializeField] private int _healthPoints = 2;

    protected override void StartAffecting(Player player)
    {
        player.PlayerHealth.RestoreHealth(_healthPoints);
        CancelAffectingWithDelay(player);
    }

    protected override void StopAffecting(Player player)
    {
        return;
    }
}
