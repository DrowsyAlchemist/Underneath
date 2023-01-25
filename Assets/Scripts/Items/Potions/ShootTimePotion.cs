using UnityEngine;

public class ShootTimePotion : Potion
{
    [SerializeField] private float _timeModifier;

    protected override void Affect(Player player)
    {
        player.Inventory.Gun.ModifyTimeBetweenShots(_timeModifier);
    }

    protected override void StopAffecting(Player player)
    {
        player.Inventory.Gun.ModifyTimeBetweenShots(1/_timeModifier);
    }
}