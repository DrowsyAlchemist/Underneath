using UnityEngine;

public class CollectableItem : Collectable
{
    [SerializeField] private Item _item;

    protected override void CollectByPlayer(Player player)
    {
        player.Inventory.AddItem(_item);
    }
}