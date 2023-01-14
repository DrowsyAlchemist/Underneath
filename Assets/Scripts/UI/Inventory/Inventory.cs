using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Inventory
{
    private List<Item> _items = new List<Item>();

    public Inventory()
    {

    }

    public Dagger Dagger { get; private set; }
    public Gun Gun { get; private set; }

    public event UnityAction<Item> ItemAdded;

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
            equippableItem.TakeOff(AccessPoint.Player);
        else
            equippableItem.Equip(AccessPoint.Player);
    }

    private void UsePotion(Potion potion)
    {
        potion.Drink(AccessPoint.Player);
    }

    private void UseKey(GoldenKey key)
    {
        Vector2 playerPosition = AccessPoint.Player.GetWorldCenter();

        if (key.CanOpen(playerPosition))
            key.OpenLock(playerPosition);
        else
            AddItem(key);
    }
}