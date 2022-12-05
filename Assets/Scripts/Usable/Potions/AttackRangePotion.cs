using UnityEngine;

public class AttackRangePotion : Potion
{
    [SerializeField] private float _rangeModifier;

    public override void Use(AccessPoint game)
    {
        if (game.Player.TryGetComponent(out GirlPlayer girlPlayer))
        {
            girlPlayer.ModifyAttackRange(_rangeModifier, Duration);
        }
        else
        {
            // Message
        }
    }
}
