using UnityEngine;

public class IncreaseDropCoinsBook : EquippableItem
{
    [SerializeField] private float _modifier;

    private CoinsSpawner _coinsSpawner;

    public override void Affect(Player player)
    {
        _coinsSpawner = AccessPoint.CoinsSpawner;
        _coinsSpawner.ModifyCoinsCount(_modifier);
    }

    public override void StopAffecting(Player player)
    {
        _coinsSpawner.ModifyCoinsCount(1/_modifier);
    }
}
