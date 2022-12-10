using UnityEngine;

public abstract class Item : MonoBehaviour
{
    [SerializeField] private ItemData _itemData;

    public ItemData ItemData => _itemData;
}
