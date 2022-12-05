using UnityEngine;

public class ExtraHeartPotion : Potion
{
    [SerializeField] private int _heartsCount;

    public override void Use(AccessPoint game)
    {
        game.Player.PlayerHealth.IncreaseMaxHealth(_heartsCount, Duration);
    }
}
