using UnityEngine;

public class HealingPotion : Potion
{
    [SerializeField] private int _healthPoints = 2;

    public override void Use(Player player)
    {
        player.PlayerHealth.RestoreHealth(_healthPoints);
    }
}
