using UnityEngine;

public class ShootTimePotion : Potion
{
    [SerializeField] private float timeModifier;

    protected override void StartAffecting(Player player)
    {
        player.Inventory.Gun.ModifyTimeBetweenShots(timeModifier);
        CancelAffectingWithDelay(player);
    }

    protected override void StopAffecting(Player player)
    {
        player.Inventory.Gun.ModifyTimeBetweenShots(1/timeModifier);
    }
}
