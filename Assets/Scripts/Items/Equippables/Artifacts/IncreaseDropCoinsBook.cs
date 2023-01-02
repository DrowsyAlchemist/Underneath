using UnityEngine;

public class IncreaseDropCoinsBook : EquippableItem
{
    [SerializeField] private float _modifier;

    public override void Affect(Player player)
    {
        CoinsSpawner.ModifyCoinsCount(_modifier);
    }

    public override void StopAffecting(Player player)
    {
        CoinsSpawner.ModifyCoinsCount(1/_modifier);
    }
}
