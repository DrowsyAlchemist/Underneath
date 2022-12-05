using UnityEngine;

public class Dagger : PermanentEffectItem
{
    [SerializeField] private int _extraDamage;

    public override void Use(AccessPoint game)
    {
        if (game.Player.TryGetComponent(out GirlPlayer girlPlayer))
            girlPlayer.IncreaseDamage(_extraDamage);
    }
}
