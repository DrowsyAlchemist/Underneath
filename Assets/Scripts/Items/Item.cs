using UnityEngine;

public abstract class Item : MonoBehaviour
{
    [SerializeField] private ItemData _itemData;
    [SerializeField] private ItemType _type;

    public ItemData Data => _itemData;
    public ItemType Type => _type;

    protected virtual void Awake()
    {
        if (TryGetComponent(out SpriteRenderer renderer)) // I need SpriteRenderer to see the sprite of the item in Inspector
            Destroy(renderer);
    }
}