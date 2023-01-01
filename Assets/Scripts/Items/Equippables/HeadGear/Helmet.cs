using UnityEngine;

public class Helmet : EquippableItem
{
    [SerializeField] private int _extraHearts;

    public override void Affect(Player player)
    {
        player.PlayerHealth.IncreaseMaxHealth(_extraHearts);
    }

    public override void StopAffecting(Player player)
    {
        player.PlayerHealth.DecreaseMaxHealth(_extraHearts);
    }
}
