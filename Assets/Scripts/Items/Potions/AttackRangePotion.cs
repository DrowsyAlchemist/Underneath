using UnityEngine;

public class AttackRangePotion : Potion
{
    [SerializeField] private float _rangeModifier;

    protected override void StartAffecting(Player player)
    {
        player.Inventory.Dagger.ModifyAttackRange(_rangeModifier);
        CancelAffectingWithDelay(player);
    }

    protected override void StopAffecting(Player player)
    {
        player.Inventory.Dagger.ModifyAttackRange(1/_rangeModifier);
    }
}
