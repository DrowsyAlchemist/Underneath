using UnityEngine;

public abstract class EquippableItem : Item
{
    [SerializeField] private EquippableItemType _type;

    public EquippableItemType Type => _type;
    public bool IsEquipped  { get; private set; }

    public void Equip(Player player)
    {
        if (IsEquipped == false)
        {
            Affect(player);
            IsEquipped = true;
        }
        else
        {
            throw new System.InvalidOperationException();
        }
    }

    public void TakeOff(Player player)
    {
        if (IsEquipped)
        {
            StopAffecting(player);
            IsEquipped = false;
        }
        else
        {
            throw new System.InvalidOperationException();
        }
    }

    protected abstract void Affect(Player player);
    protected abstract void StopAffecting(Player player);
}
