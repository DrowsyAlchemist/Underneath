using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] private AccessPoint _game;

    [SerializeField] private RectTransform _itemsContainer;
    [SerializeField] private ItemRenderer _itemRenderer;

    [SerializeField] private Image _descriptionImage;
    [SerializeField] private TMP_Text _lable;
    [SerializeField] private TMP_Text _description;
    [SerializeField] private Button _useButton;

    [SerializeField] private PotionActivator _activePotionsView;
    [SerializeField] private PlayerSlots _playerSlots;

    private List<Item> _items = new List<Item>();
    private ItemRenderer _highlightedItem;

    private void OnEnable()
    {
        _useButton.onClick.AddListener(OnUseButtonClick);
    }

    private void OnDisable()
    {
        _useButton.onClick.RemoveListener(OnUseButtonClick);
        ClearDescription();
    }

    private void Start()
    {
        ClearDescription();
    }

    public void AddItem(Item item)
    {
        _items.Add(item);
        var itemRenderer = Instantiate(_itemRenderer, _itemsContainer);
        itemRenderer.Render(item);
        itemRenderer.ButtonClicked += OnItemClick;
    }

    private void OnItemClick(ItemRenderer itemRenderer)
    {
        _highlightedItem = itemRenderer;
        RenderDescription(itemRenderer.Item);
    }

    private void RenderDescription(Item item)
    {
        _descriptionImage.sprite = item.ItemData.Sprite;
        _descriptionImage.color = Color.white;
        _lable.text = item.ItemData.Lable;
        _description.text = item.ItemData.Description;
        _useButton.interactable = true;
    }

    private void ClearDescription()
    {
        _descriptionImage.color = Color.clear;
        _lable.text = "";
        _description.text = "";
        _useButton.interactable = false;
    }

    private void OnUseButtonClick()
    {
        if (_highlightedItem.Item.TryGetComponent(out EquippableItem equippableItem))
        {
            if (_highlightedItem.IsInPlayerSlot)
            {
                TakeOffItem(_highlightedItem);
                return;
            }
            if (_playerSlots.CanEquipItem(equippableItem) == false)
            {
                ItemRenderer itemRenderer = _playerSlots.GetItemOfType(equippableItem.Type);
                TakeOffItem(itemRenderer);
            }
            _playerSlots.SetItem(_highlightedItem);
            equippableItem.Affect(_game.Player);
        }
        else if (_highlightedItem.Item.TryGetComponent(out Potion potion))
        {
            _activePotionsView.UsePotion(_highlightedItem);
        }
        ClearDescription();
    }

    private void TakeOffItem(ItemRenderer itemRenderer)
    {
        itemRenderer.transform.SetParent(_itemsContainer);
        itemRenderer.Item.GetComponent<EquippableItem>().StopAffecting(_game.Player);
    }
}