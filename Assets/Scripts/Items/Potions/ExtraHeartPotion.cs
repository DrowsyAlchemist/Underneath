using UnityEngine;

public class ExtraHeartPotion : Potion
{
    [SerializeField] private int _heartsCount;

    protected override void StartAffecting(Player player)
    {
        player.Health.IncreaseMaxHealth(_heartsCount);
    }

    protected override void StopAffecting(Player player)
    {
        player.Health.DecreaseMaxHealth(_heartsCount);
    }
}
