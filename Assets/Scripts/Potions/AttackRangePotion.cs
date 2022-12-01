using UnityEngine;

public class AttackRangePotion : Potion
{
    [SerializeField] private float _rangeModifier;

    public override void Use(Player player)
    {
        if (player.TryGetComponent(out GirlPlayer girlPlayer))
        {
            girlPlayer.ModifyAttackRange(_rangeModifier);
        }
        else
        {
            // Message
        }
    }
}
