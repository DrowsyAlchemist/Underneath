using UnityEngine;

public class Dagger : EquippableItem
{
    [SerializeField] private int _extraDamage;

    public override void Affect(Player player)
    {
        if (player.TryGetComponent(out GirlPlayer girlPlayer))
            girlPlayer.IncreaseDamage(_extraDamage);
    }

    public override void StopAffecting(Player player)
    {
        if (player.TryGetComponent(out GirlPlayer girlPlayer))
            girlPlayer.DecreaseDamage(_extraDamage);
    }
}
