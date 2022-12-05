using UnityEngine;

public class Armor : PermanentEffectItem
{
    [SerializeField] private int _extraHearts;

    public override void Use(AccessPoint game)
    {
        game.Player.PlayerHealth.IncreaseMaxHealth(_extraHearts);
    }
}
