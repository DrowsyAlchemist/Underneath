using UnityEngine;

public abstract class EquippableItem : Item
{
    [SerializeField] private EquippableItemType _type;

    public EquippableItemType Type => _type;

    public abstract void Affect(Player player);
    public abstract void StopAffecting(Player player);
}
