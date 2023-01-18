using UnityEngine;

public class CollectableItem : Bubble
{
    [SerializeField] private Item _item;

    protected override void Collect(Player player)
    {
        player.Inventory.AddItem(Instantiate(_item));
    }
}