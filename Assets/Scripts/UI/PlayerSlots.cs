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
        if (itemRenderer.Item.TryGetComponent(out EquippableItem item) == false)
            throw new ArgumentException("Item should be equippable.");

        if (CanEquipItem(item) == false)
            throw new InvalidOperationException($"The slot for type {item.Type} is already occupied.");

        switch (item.Type)
        {
            case EquippableItemType.HeadGear:
                itemRenderer.transform.SetParent(_headGearSlot);
                break;
            case EquippableItemType.BodyArmor:
                itemRenderer.transform.SetParent(_bodyArmorSlot);
                break;
            case EquippableItemType.MeleeWeapon:
                itemRenderer.transform.SetParent(_meleeWeaponSlot);
                break;
            case EquippableItemType.Gun:
                itemRenderer.transform.SetParent(_gunSlot);
                break;
            case EquippableItemType.Artifact:
                itemRenderer.transform.SetParent(GetEmptyArtifactSlot());
                break;
            default:
                throw new ArgumentException($"Type {item.Type} is invalid.");
        }
    }

    public bool CanEquipItem(EquippableItem item)
    {
        return item.Type switch
        {
            EquippableItemType.HeadGear => _headGearSlot.transform.childCount == 0,
            EquippableItemType.BodyArmor => _bodyArmorSlot.transform.childCount == 0,
            EquippableItemType.MeleeWeapon => _meleeWeaponSlot.transform.childCount == 0,
            EquippableItemType.Gun => _gunSlot.transform.childCount == 0,
            EquippableItemType.Artifact => CanEquipArtifact(),
            _ => throw new Exception($"Type {item.Type} is invalid."),
        };
    }

    public ItemRenderer GetItemOfType(EquippableItemType type)
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

    private ItemRenderer FindItemOfType(EquippableItemType type)
    {
        return type switch
        {
            EquippableItemType.HeadGear => _headGearSlot.GetChild(0).GetComponent<ItemRenderer>(),
            EquippableItemType.BodyArmor => _bodyArmorSlot.GetChild(0).GetComponent<ItemRenderer>(),
            EquippableItemType.MeleeWeapon => _meleeWeaponSlot.GetChild(0).GetComponent<ItemRenderer>(),
            EquippableItemType.Gun => _gunSlot.GetChild(0).GetComponent<ItemRenderer>(),
            EquippableItemType.Artifact => _artifactsSlots[^1].GetChild(0).GetComponent<ItemRenderer>(),
            _ => throw new InvalidOperationException($"Type {type} is invalid."),
        };
    }
}
