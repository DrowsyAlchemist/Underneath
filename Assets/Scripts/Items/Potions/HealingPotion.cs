using UnityEngine;

public class HealingPotion : Potion
{
    [SerializeField] private int _healthPoints = 2;

    //public override void Drink(Player player)
    //{
    //    player.PlayerHealth.RestoreHealth(_healthPoints);
    //}

    public override void StartAffecting(Player player)
    {
        throw new System.NotImplementedException();
    }

    public override void StopAffecting(Player player)
    {
        throw new System.NotImplementedException();
    }
}
