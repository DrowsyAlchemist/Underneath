using UnityEngine;

public class CollectableItem : Bubble
{
    [SerializeField] private Item _itemTemplate;

    protected override void Collect(Player player)
    {
        player.Inventory.AddItem(_itemTemplate);
    }
}