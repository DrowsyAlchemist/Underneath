using UnityEngine;

public class ShootTimePotion : Potion
{
    [SerializeField] private float timeModifier;

    public override void Use(AccessPoint game)
    {
        if (game.Player.TryGetComponent(out GirlPlayer girlPlayer))
        {
            girlPlayer.ModifyTimeBetweenShots(timeModifier, Duration);
        }
        else
        {
            // Message
        }
    }
}
