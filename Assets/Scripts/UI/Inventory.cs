using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] private AccessPoint _accessPoint;

    [SerializeField] private RectTransform _itemsContainer;
    [SerializeField] private ItemRenderer _itemRenderer;

    [SerializeField] private Image _descriptionImage;
    [SerializeField] private TMP_Text _lable;
    [SerializeField] private TMP_Text _description;
    [SerializeField] private Button _useButton;

    [SerializeField] private RectTransform _activePotionsContainer;
    [SerializeField] private PlayerSlots _playerSlots;

    [SerializeField] private List<Item> _defaultItems = new List<Item>();

    private List<Item> _items = new List<Item>();
    private ItemRenderer _highlightedItem;

    public Dagger Dagger { get; private set; }
    public Gun Gun { get; private set; }

    public void SetDagger(Dagger dagger)
    {
        if (dagger == null)
            throw new System.ArgumentNullException();

        Dagger = dagger;
    }

    public void TakeOffDagger()
    {
        Dagger = null;
    }

    public void SetGun(Gun gun)
    {
        if (gun == null)
            throw new System.ArgumentNullException();

        Gun = gun;
    }

    public void TakeOffGun()
    {
        Gun = null;
    }

    private void OnEnable()
    {
        _useButton.onClick.AddListener(OnUseButtonClick);
    }

    private void Start()
    {
        foreach (var item in _defaultItems)
            AddItem(item);
    }

    private void OnDisable()
    {
        _useButton.onClick.RemoveListener(OnUseButtonClick);
        ClearDescription();
    }

    public void AddItem(Item item)
    {
        _items.Add(item);
        var itemRenderer = Instantiate(_itemRenderer, _itemsContainer);

        //if (item is Potion)
        //item = Instantiate(item, itemRenderer.transform);

        if (item is EquippableItem equippableItem)
        {
            if (equippableItem.Type == EquippableItemType.MeleeWeapon
                || equippableItem.Type == EquippableItemType.Gun)
            {
                item = Instantiate(item, _accessPoint.Player.transform);
            }
        }
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
            equippableItem.Affect(_accessPoint.Player);
        }
        else if (_highlightedItem.Item.TryGetComponent(out Potion potion))
        {
            Destroy(_highlightedItem.gameObject);
            potion.Drink(_accessPoint.Player, _activePotionsContainer);
        }
        ClearDescription();
    }

    private void TakeOffItem(ItemRenderer itemRenderer)
    {
        itemRenderer.transform.SetParent(_itemsContainer);
        itemRenderer.Item.GetComponent<EquippableItem>().StopAffecting(_accessPoint.Player);
    }
}