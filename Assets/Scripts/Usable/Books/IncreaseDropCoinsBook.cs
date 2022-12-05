using UnityEngine;

public class IncreaseDropCoinsBook : PermanentEffectItem
{
    [SerializeField] private float _modifier;

    public override void Use(AccessPoint game)
    {
        game.CoinsSpawner.IncreaseCoinsCount(_modifier);
    }
}
