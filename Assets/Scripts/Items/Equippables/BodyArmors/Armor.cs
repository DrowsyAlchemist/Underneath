using UnityEngine;

public class Armor : EquippableItem
{
    [SerializeField] private int _extraHearts;

    protected override void Affect(Player player)
    {
        player.PlayerHealth.IncreaseMaxHealth(_extraHearts);
    }

    protected override void StopAffecting(Player player)
    {
        player.PlayerHealth.DecreaseMaxHealth(_extraHearts);
    }
}
