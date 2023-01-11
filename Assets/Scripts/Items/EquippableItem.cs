using UnityEngine;

public abstract class EquippableItem : Item
{
    [SerializeField] private EquippableItemType _type;

    public EquippableItemType Type => _type;
    public bool IsEquipped { get; private set; }

    public void Equip(Player player)
    {
        Affect(player);
        IsEquipped = true;
    }

    public void TakeOff(Player player)
    {
        StopAffecting(player);
        IsEquipped = false;
    }

    protected abstract void Affect(Player player);
    protected abstract void StopAffecting(Player player);
}
