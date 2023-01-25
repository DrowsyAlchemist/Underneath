using UnityEngine;

public class IncreaseDamagePotion : Potion
{
    [SerializeField] private int _extraDamage;

    protected override void Affect(Player player)
    {
        player.Inventory.Dagger.IncreaseDamage(_extraDamage);
    }

    protected override void StopAffecting(Player player)
    {
        player.Inventory.Dagger.DecreaseDamage(_extraDamage);
    }
}