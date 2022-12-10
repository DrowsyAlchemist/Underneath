using UnityEngine;

public class ShootTimePotion : Potion
{
    [SerializeField] private float timeModifier;

    //public override void Drink(Player player)
    //{
    //    if (player.TryGetComponent(out GirlPlayer girlPlayer))
    //    {
    //        girlPlayer.ModifyTimeBetweenShots(timeModifier, Duration);
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
