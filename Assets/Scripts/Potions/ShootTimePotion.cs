using UnityEngine;

public class ShootTimePotion : Potion
{
    [SerializeField] private float timeModifier;

    public override void Use(Player player)
    {
        if (player.TryGetComponent(out GirlPlayer girlPlayer))
        {
            girlPlayer.ModifyTimeBetweenShots(timeModifier);
        }
        else
        {
            // Message
        }
    }
}
