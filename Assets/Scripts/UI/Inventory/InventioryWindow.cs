using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventioryWindow : MonoBehaviour, ISaveable
{
    [SerializeField] private Player _player;

    [SerializeField] private ItemRenderer _itemRenderer;
    [SerializeField] private RectTransform _itemsContainer;
    [SerializeField] private ActivePotionsView _activePotionsView;
    [SerializeField] private PlayerSlots _playerSlots;

    [SerializeField] private ItemDescriptionRenderer _itemDescription;
    [SerializeField] private Button _useButton;

    private ItemRenderer _highlightedItem;
    private List<ItemRenderer> _itemRenderers = new List<ItemRenderer>();

    public Inventory _inventory;

    public void Save()
    {
        _inventory.Save();
    }

    private void Start()
    {
        _inventory = _player.Inventory;
        _inventory.ItemAdded += OnItemAdded;
        _inventory.Cleared += OnInventoryCleared;
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        _inventory.ItemAdded -= OnItemAdded;
        _inventory.Cleared -= OnInventoryCleared;

        for (int i = 0; i < _itemsContainer.childCount; i++)
            _itemsContainer.GetChild(i).GetComponent<ItemRenderer>().ButtonClicked -= OnItemClick;
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

    private void OnInventoryCleared()
    {
        if (_itemRenderers.Count > 0)
        {
            foreach (var itemRenderer in _itemRenderers)
            {
                TakeOffItem(itemRenderer);
                Destroy(itemRenderer.gameObject);
            }
            _itemRenderers.Clear();
        }
    }

    private void OnItemAdded(Item item, bool isEquipped)
    {
        if (item.name.Contains("Clone") == false)
            item = Instantiate(item);

        var itemRenderer = Instantiate(_itemRenderer, _itemsContainer);
        _itemRenderers.Add(itemRenderer);
        itemRenderer.Render(item);
        itemRenderer.ButtonClicked += OnItemClick;

        if (isEquipped)
        {
            ((AffectingItem)item).SetAffecting();
            _playerSlots.SetItem(itemRenderer);
        }
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
            case AffectingItem:
                SetAffectingItem(_highlightedItem);
                break;
            case GoldenKey:
                _inventory.KeyUsed += OnKeyUsed;
                break;
        }
        _inventory.UseItem(_highlightedItem.Item);
    }

    private void SetAffectingItem(ItemRenderer itemRenderer)
    {
        if (itemRenderer.Item is Potion)
            SetPotion(itemRenderer);
        else
            EquipItem(itemRenderer);
    }

    private void SetPotion(ItemRenderer itemRenderer)
    {
        var potion = itemRenderer.Item as Potion;
        _activePotionsView.SetPotion(potion);
        UISounds.PlayDrinkPotion();
        Destroy(itemRenderer.gameObject);
    }

    private void EquipItem(ItemRenderer itemRenderer)
    {
        var itemToEquip = itemRenderer.Item as AffectingItem;

        if (itemToEquip.IsAffecting)
        {
            TakeOffItem(itemRenderer);
        }
        else
        {
            if (_playerSlots.CanEquipItem(itemToEquip) == false)
            {
                var equippedItemRenderer = _playerSlots.GetItemOfType(itemToEquip.Type);
                TakeOffItem(equippedItemRenderer);
                _inventory.UseItem(equippedItemRenderer.Item);
            }
            _playerSlots.SetItem(itemRenderer);
        }
        UISounds.PlayEquip();
    }

    private void TakeOffItem(ItemRenderer itemRenderer)
    {
        itemRenderer.transform.SetParent(_itemsContainer);
    }

    private void OnKeyUsed(bool result)
    {
        if (result)
        {
            _inventory.KeyUsed -= OnKeyUsed;

            if (_highlightedItem != null && _highlightedItem.Item is GoldenKey)
            {
                Destroy(_highlightedItem);
                gameObject.SetActive(false);
                Time.timeScale = 1;
            }
            else
            {
                throw new System.Exception("Valid itemRenderer is not found.");
            }
        }
        else
        {
            string message = "I don't see the gates. Maybe I should get closer.";
            MessageCreator.ShowMessage(message, (RectTransform)transform, MessageType.Message);
        }
    }
}