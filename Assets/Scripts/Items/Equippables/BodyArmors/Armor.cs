using UnityEngine;

public class Armor : AffectingItem
{
    [SerializeField] private int _extraHearts;

    protected override void StartAffecting(Player player)
    {
        player.Health.IncreaseMaxHealth(_extraHearts);
    }

    protected override void StopAffecting(Player player)
    {
        player.Health.DecreaseMaxHealth(_extraHearts);
    }
}