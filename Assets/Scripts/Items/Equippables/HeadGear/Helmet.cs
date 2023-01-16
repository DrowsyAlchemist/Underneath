using UnityEngine;

public class Helmet : AffectingItem
{
    [SerializeField] private int _extraHearts;

    protected override void StartAffecting(Player player)
    {
        player.PlayerHealth.IncreaseMaxHealth(_extraHearts);
    }

    protected override void StopAffecting(Player player)
    {
        player.PlayerHealth.DecreaseMaxHealth(_extraHearts);
    }
}