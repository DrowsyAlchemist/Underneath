using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    private const string SavesFolder = "Inventory";
    private const string SaveFileName = "Items";
    private const string ItemsFolder = "Items";
    private static Inventory _instance;
    private readonly Player _player;
    private readonly List<Item> _items = new List<Item>();

    public bool IsEmpty => _items.Count == 0;
    public Dagger Dagger { get; private set; }
    public Gun Gun { get; private set; }

    public event Action Cleared;
    public event Action<Item, bool> ItemAdded;
    public event Action<bool> KeyUsed;

    private Inventory(Player player)
    {
        _player = player;
    }

    public static Inventory LoadLastSaveOrDefault(Player player)
    {
        if (player == null)
            throw new ArgumentNullException();

        if (_instance == null)
            _instance = new Inventory(player);

        _instance.Reset();
        return _instance;
    }

    private void Reset()
    {
        _items.Clear();
        Dagger = null;
        Gun = null;
        Cleared?.Invoke();

        var loadedItems = SaveLoadManager.GetLoadOrDefault<List<ItemSave>>(SavesFolder, SaveFileName);

        if (loadedItems == null)
            return;

        foreach (var itemSave in loadedItems)
        {
            string localPath = ItemsFolder + "/" + itemSave.FileName;
            Item item = Resources.Load<Item>(localPath);

            if (item == null)
                throw new System.Exception($"Can't find valid item on path: " + localPath);

            AddItemWithFlag(item, itemSave.IsAffecting);
        }
    }

    public void Save()
    {
        List<ItemSave> itemsToSave = new();

        foreach (Item item in _items)
        {
            string fileName = item.Data.SaveFileName;
            bool isEquipped = false;

            if (item is AffectingItem affectingItem)
                if (affectingItem.IsAffecting)
                    isEquipped = true;

            var itemSave = new ItemSave(fileName, isEquipped);
            itemsToSave.Add(itemSave);
        }
        SaveLoadManager.Save("Inventory", "Items", itemsToSave);
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
        item.transform.SetParent(_player.transform);
        AddItemWithFlag(item);
    }

    private void AddItemWithFlag(Item item, bool isAffecting = false)
    {
        _items.Add(item);
        ItemAdded?.Invoke(item, isAffecting);
    }

    public void UseItem(Item item)
    {
        switch (item)
        {
            case AffectingItem affectingItem:
                UseAffectingItem(affectingItem);
                break;
            case GoldenKey key:
                UseKey(key);
                break;
            default:
                throw new InvalidOperationException();
        }
    }

    private void UseAffectingItem(AffectingItem affectingItem)
    {
        if (affectingItem.IsAffecting)
            affectingItem.CancelEffect(_player);
        else
            affectingItem.ApplyEffect(_player);

        if (affectingItem.Type == ItemType.Potion)
            _items.Remove(affectingItem);
    }

    private void UseKey(GoldenKey key)
    {
        bool result = key.TryOpenGates(_player);
        KeyUsed?.Invoke(result);
    }

    [System.Serializable]
    private class ItemSave
    {
        public string FileName;
        public bool IsAffecting;

        public ItemSave(string fileName, bool isAffecting)
        {
            FileName = fileName;
            IsAffecting = isAffecting;
        }
    }
}