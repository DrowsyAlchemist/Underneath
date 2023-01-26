using UnityEngine;

public class ShootTimePotion : Potion
{
    [SerializeField] private float _timeModifier;

    private Gun _gun;

    protected override void StartAffecting(Player player)
    {
        _gun = player.Inventory.Gun;

        if (_gun != null)
            _gun.ModifyTimeBetweenShots(_timeModifier);
    }

    protected override void StopAffecting(Player player)
    {
        if (_gun != null)
        {
            _gun.ModifyTimeBetweenShots(1 / _timeModifier);
            _gun = null;
        }
    }

    private void OnValidate()
    {
        if (_timeModifier <= 0)
            _timeModifier = 1;
    }
}