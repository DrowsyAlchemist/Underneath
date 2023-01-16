using UnityEngine;

public class IncreaseDropCoinsBook : AffectingItem
{
    [SerializeField] private float _modifier;

    protected override void StartAffecting(Player player)
    {
        CoinsSpawner.ModifyCoinsCount(_modifier);
    }

    protected override void StopAffecting(Player player)
    {
        CoinsSpawner.ModifyCoinsCount(1/_modifier);
    }
}