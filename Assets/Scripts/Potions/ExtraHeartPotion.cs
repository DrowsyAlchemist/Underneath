using UnityEngine;

public class ExtraHeartPotion : Potion
{
    [SerializeField] private int _heartsCount;
    public override void Use(Player player)
    {
        player.PlayerHealth.IncreaseMaxHealth(_heartsCount);
    }
}
