using UnityEngine;

public class IncreaseDamagePotion : Potion
{
    [SerializeField] private int extraDamage;

    public override void Use(AccessPoint game)
    {
        if (game.Player.TryGetComponent(out GirlPlayer girlPlayer))
        {
            girlPlayer.IncreaseDamage(extraDamage, Duration);
        }
        else
        {
            // Message
        }
    }
}
