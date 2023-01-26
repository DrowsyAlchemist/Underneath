using UnityEngine;

public class AttackRangePotion : Potion
{
    [SerializeField] private float _rangeModifier;

    private Dagger _dagger;

    protected override void StartAffecting(Player player)
    {
        _dagger = player.Inventory.Dagger;

        if (_dagger != null)
            _dagger.ModifyAttackRange(_rangeModifier);
    }

    protected override void StopAffecting(Player player)
    {
        if (_dagger != null)
        {
            _dagger.ModifyAttackRange(1 / _rangeModifier);
            _dagger = null;
        }
    }

    private void OnValidate()
    {
        if (_rangeModifier <= 0)
            _rangeModifier = 1;
    }
}
