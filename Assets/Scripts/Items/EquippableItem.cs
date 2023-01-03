using UnityEngine;

public abstract class EquippableItem : Item
{
    [SerializeField] private EquippableItemType _type;
    public EquippableItemType Type => _type;

    public void Equip(Player player)
    {
        Affect(player);
    }

    public void TakeOff(Player player)
    {
        StopAffecting(player);
    }

    protected abstract void Affect(Player player);
    protected abstract void StopAffecting(Player player);
}
