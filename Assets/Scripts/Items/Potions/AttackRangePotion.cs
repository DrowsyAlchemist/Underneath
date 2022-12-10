using UnityEngine;

public class AttackRangePotion : Potion
{
    [SerializeField] private float _rangeModifier;

    //public override void Drink(Player player)
    //{
    //    if (player.TryGetComponent(out GirlPlayer girlPlayer))
    //    {
    //        girlPlayer.ModifyAttackRange(_rangeModifier, Duration);
    //    }
    //    else
    //    {
    //        // Message
    //    }
    //}

    public override void StartAffecting(Player player)
    {
        throw new System.NotImplementedException();
    }

    public override void StopAffecting(Player player)
    {
        throw new System.NotImplementedException();
    }
}
