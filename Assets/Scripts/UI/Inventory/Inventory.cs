using System;
using System.Collections.Generic;

public class Inventory
{
    private readonly Player _player;
    private readonly List<Item> _items = new List<Item>();

    public Dagger Dagger { get; private set; }
    public Gun Gun { get; private set; }

    public event Action<Item> ItemAdded;
    public event Action<bool> KeyUsed;

    public Inventory(Player player)
    {
        _player = player;
    }

    public void SetDagger(Dagger dagger)
    {
        Dagger = dagger ?? throw new System.ArgumentNullException();
    }

    public void TakeOffDagger()
    {
        Dagger = null;
    }

    public void SetGun(Gun gun)
    {
        Gun = gun ?? throw new System.ArgumentNullException();
    }

    public void TakeOffGun()
    {
        Gun = null;
    }

    public void AddItem(Item item)
    {
        _items.Add(item);
        ItemAdded?.Invoke(item);
    }

    public void UseItem(Item item)
    {
        switch (item)
        {
            case EquippableItem equippableItem:
                UseEquippableItem(equippableItem);
                break;
            case Potion potion:
                UsePotion(potion);
                break;
            case GoldenKey key:
                UseKey(key);
                break;
        }
    }

    private void UseEquippableItem(EquippableItem equippableItem)
    {
        if (equippableItem.IsEquipped)
            equippableItem.TakeOff(_player);
        else
            equippableItem.Equip(_player);
    }

    private void UsePotion(Potion potion)
    {
        potion.Drink(_player);
    }

    private void UseKey(GoldenKey key)
    {
        bool result = key.TryOpenGates(_player);
        KeyUsed?.Invoke(result);
    }
}