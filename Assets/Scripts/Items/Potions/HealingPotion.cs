using UnityEngine;

public class HealingPotion : Potion
{
    [SerializeField] private int _healthPoints = 2;

    protected override void Affect(Player player)
    {
        player.Health.RestoreHealth(_healthPoints);
    }

    protected override void StopAffecting(Player player)
    {
        return;
    }
}
