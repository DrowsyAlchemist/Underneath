using UnityEngine;

public abstract class Item : MonoBehaviour
{
    [SerializeField] private ItemData _itemData;

    public ItemData Data => _itemData;

    private void Start()
    {
        if (gameObject.TryGetComponent(out SpriteRenderer spriteRenderer))
            Destroy(spriteRenderer);
    }
}
