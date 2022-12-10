using UnityEngine;

public class IncreaseDamagePotion : Potion
{
    [SerializeField] private int extraDamage;

    //public override void Drink(Player player)
    //{
    //    if (player.TryGetComponent(out GirlPlayer girlPlayer))
    //    {
    //        girlPlayer.IncreaseDamage(extraDamage, Duration);
    //    }
    //    else
    //    {
    //        //Message
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
