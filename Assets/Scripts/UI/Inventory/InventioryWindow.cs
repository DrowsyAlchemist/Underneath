using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventioryWindow : MonoBehaviour
{
    [SerializeField] private RectTransform _itemsContainer;
    [SerializeField] private ItemRenderer _itemRenderer;
    [SerializeField] private ItemDescription _itemDescription;
    [SerializeField] private Button _useButton;

    [SerializeField] private ActivePotionsView _activePotionsView;

    [SerializeField] private PlayerSlots _playerSlots;

    [SerializeField] private List<Item> _defaultItems;

    private ItemRenderer _highlightedItem;

    public Inventory Inventory { get; private set; }

    private void Start()
    {
        Inventory = new Inventory(AccessPoint.Player);
        Inventory.ItemAdded += AddItem;

        foreach (var item in _defaultItems)
            Inventory.AddItem(item);

        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        Inventory.ItemAdded -= AddItem;
    }

    private void OnEnable()
    {
        _useButton.onClick.AddListener(OnUseButtonClick);
        _useButton.interactable = false;
    }

    private void OnDisable()
    {
        _useButton.onClick.RemoveListener(OnUseButtonClick);

        if (_highlightedItem != null)
            _highlightedItem.SetHighlighted(false);
    }

    private void AddItem(Item item)
    {
        var itemRenderer = Instantiate(_itemRenderer, _itemsContainer);
        //////////////////////////////////////////////////////////////////////
        //if (item is EquippableItem equippableItem)
        //{
        //    if (equippableItem.Type == EquippableItemType.MeleeWeapon
        //        || equippableItem.Type == EquippableItemType.Gun)
        //    {
        //        item = Instantiate(item, AccessPoint.Player.transform);
        //    }
        //}
        /////////////////////////////////////////////////////////////////////
        itemRenderer.Render(item);
        itemRenderer.ButtonClicked += OnItemClick;
    }

    private void OnItemClick(ItemRenderer itemRenderer)
    {
        if (_highlightedItem != null)
            _highlightedItem.SetHighlighted(false);

        _highlightedItem = itemRenderer;
        _highlightedItem.SetHighlighted(true);
        _itemDescription.Render(itemRenderer.Item);
        _useButton.interactable = true;
    }

    private void OnUseButtonClick()
    {
        _highlightedItem.SetHighlighted(false);
        _itemDescription.Clear();
        _useButton.interactable = false;

        switch (_highlightedItem.Item)
        {
            case EquippableItem:
                SetEquippableItem(_highlightedItem);
                break;
            case Potion:
                SetPotion(_highlightedItem);
                break;
            case GoldenKey:
                DestroyKey(_highlightedItem);
                break;
        }
        Inventory.UseItem(_highlightedItem.Item);
    }

    private void SetEquippableItem(ItemRenderer itemRenderer)
    {
        var item = itemRenderer.Item as EquippableItem;

        if (item.IsEquipped)
        {
            TakeOffItem(_highlightedItem);
        }
        else
        {
            if (_playerSlots.CanEquipItem(item) == false)
                TakeOffItem(_playerSlots.GetItemOfType(item.Type));

            _playerSlots.SetItem(_highlightedItem);
        }
        UISounds.PlayEquip();
    }

    private void SetPotion(ItemRenderer itemRenderer)
    {
        var potion = itemRenderer.Item as Potion;
        _activePotionsView.SetPotion(potion);
        UISounds.PlayDrinkPotion();
        Destroy(itemRenderer.gameObject);
    }

    private void DestroyKey(ItemRenderer itemRenderer)
    {
        Destroy(itemRenderer.gameObject);
        gameObject.SetActive(false);
    }

    private void TakeOffItem(ItemRenderer itemRenderer)
    {
        itemRenderer.transform.SetParent(_itemsContainer);
    }
}