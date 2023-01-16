using UnityEngine;

public class IncreaseDamagePotion : Potion
{
    [SerializeField] private int extraDamage;

    protected override void StartAffecting(Player player)
    {
        player.Inventory.Dagger.IncreaseDamage(extraDamage);
        CancelAffectingWithDelay(player);
    }

    protected override void StopAffecting(Player player)
    {
        player.Inventory.Dagger.DecreaseDamage(extraDamage);
    }
}
