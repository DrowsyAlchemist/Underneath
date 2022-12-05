using UnityEngine;

public class HealingPotion : UseableItem
{
    [SerializeField] private int _healthPoints = 2;

    public override void Use(AccessPoint game)
    {
        game.Player.PlayerHealth.RestoreHealth(_healthPoints);
    }
}
