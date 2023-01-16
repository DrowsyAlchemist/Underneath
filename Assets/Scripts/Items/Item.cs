using UnityEngine;

public abstract class Item : MonoBehaviour
{
    [SerializeField] private ItemData _itemData;
    [SerializeField] private ItemType _type;

    public ItemData Data => _itemData;
    public ItemType Type => _type;
}
