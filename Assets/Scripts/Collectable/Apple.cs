using UnityEngine;

public class Apple : Collectable
{
    [SerializeField] private int _restoringHealth = 1;

    protected override void CollectByPlayer(Player player)
    {
        player.PlayerHealth.RestoreHealth(_restoringHealth);
    }
}
