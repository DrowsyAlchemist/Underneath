using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseDamagePotion : Potion
{
    [SerializeField] private int extraDamage;

    public override void Use(Player player)
    {
        if (player.TryGetComponent(out GirlPlayer girlPlayer))
        {
            girlPlayer.IncreaseDamage(extraDamage);
        }
        else
        {
            // Message
        }
    }
}
