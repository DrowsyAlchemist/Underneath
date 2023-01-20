using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlots : MonoBehaviour
{
    [SerializeField] private RectTransform _headGearSlot;
    [SerializeField] private RectTransform _bodyArmorSlot;
    [SerializeField] private RectTransform _meleeWeaponSlot;
    [SerializeField] private RectTransform _gunSlot;
    [SerializeField] private List<RectTransform> _artifactsSlots;

    public void SetItem(ItemRenderer itemRenderer)
    {
        var item = itemRenderer.Item;

        if (CanEquipItem(item) == false)
            throw new InvalidOperationException($"The slot for type {item.Type} is already occupied.");

        RectTransform container = item.Type switch
        {
            ItemType.HeadGear => _headGearSlot,
            ItemType.BodyArmor => _bodyArmorSlot,
            ItemType.MeleeWeapon => _meleeWeaponSlot,
            ItemType.Gun => _gunSlot,
            ItemType.Artifact => GetEmptyArtifactSlot(),
            _ => throw new ArgumentException($"Type {item.Type} is invalid."),
        };
        itemRenderer.transform.SetParent(container);
    }

    public bool CanEquipItem(Item item)
    {
        return item.Type switch
        {
            ItemType.HeadGear => _headGearSlot.transform.childCount == 0,
            ItemType.BodyArmor => _bodyArmorSlot.transform.childCount == 0,
            ItemType.MeleeWeapon => _meleeWeaponSlot.transform.childCount == 0,
            ItemType.Gun => _gunSlot.transform.childCount == 0,
            ItemType.Artifact => CanEquipArtifact(),
            _ => throw new Exception($"Type {item.Type} is invalid."),
        };
    }

    public ItemRenderer GetItemOfType(ItemType type)
    {
        try
        {
            return FindItemOfType(type);
        }
        catch (Exception e)
        {
            throw new InvalidOperationException($"There is no item of type {type}.\n" + e.ToString());
        }
    }

    private bool CanEquipArtifact()
    {
        foreach (var slot in _artifactsSlots)
        {
            if (slot.transform.childCount == 0)
                return true;
        }
        return false;
    }

    private RectTransform GetEmptyArtifactSlot()
    {
        foreach (var slot in _artifactsSlots)
            if (slot.transform.childCount == 0)
                return slot;

        throw new Exception($"There are no empty artifact slots.");
    }

    private ItemRenderer FindItemOfType(ItemType type)
    {
        return type switch
        {
            ItemType.HeadGear => _headGearSlot.GetChild(0).GetComponent<ItemRenderer>(),
            ItemType.BodyArmor => _bodyArmorSlot.GetChild(0).GetComponent<ItemRenderer>(),
            ItemType.MeleeWeapon => _meleeWeaponSlot.GetChild(0).GetComponent<ItemRenderer>(),
            ItemType.Gun => _gunSlot.GetChild(0).GetComponent<ItemRenderer>(),
            ItemType.Artifact => _artifactsSlots[^1].GetChild(0).GetComponent<ItemRenderer>(),
            _ => throw new InvalidOperationException($"Type {type} is invalid."),
        };
    }
}