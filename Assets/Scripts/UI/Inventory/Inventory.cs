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
        Dagger = dagger ?? throw new System.ArgumentNullException();
    }

    public void TakeOffDagger()
    {
        Dagger = null;
    }

    public void SetGun(Gun gun)
    {
        Gun = gun ?? throw new System.ArgumentNullException();
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

        if (_highlightedItem != null)
            _highlightedItem.SetHighlighted(false);

        ClearDescription();
    }

    public void AddItem(Item item)
    {
        _items.Add(item);
        var itemRenderer = Instantiate(_itemRenderer, _itemsContainer);

        if (item is EquippableItem equippableItem)
        {
            if (equippableItem.Type == EquippableItemType.MeleeWeapon
                || equippableItem.Type == EquippableItemType.Gun)
            {
                item = Instantiate(item, AccessPoint.Player.transform);
            }
        }
        itemRenderer.Render(item);
        itemRenderer.ButtonClicked += OnItemClick;
    }

    private void OnItemClick(ItemRenderer itemRenderer)
    {
        if (_highlightedItem != null)
            _highlightedItem.SetHighlighted(false);

        _highlightedItem = itemRenderer;
        _highlightedItem.SetHighlighted(true);
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
        _highlightedItem.SetHighlighted(false);
        ClearDescription();

        if (_highlightedItem.Item.TryGetComponent(out EquippableItem equippableItem))
        {
            if (_highlightedItem.transform.parent != _itemsContainer)
            {
                TakeOffItem(_highlightedItem);
                return;
            }
            if (_playerSlots.CanEquipItem(equippableItem) == false)
            {
                ItemRenderer itemRenderer = _playerSlots.GetItemOfType(equippableItem.Type);
                TakeOffItem(itemRenderer);
            }
            UISounds.PlayEquip();
            _playerSlots.SetItem(_highlightedItem);
            equippableItem.Equip(AccessPoint.Player);
        }
        else if (_highlightedItem.Item.TryGetComponent(out Potion potion))
        {
            UISounds.PlayDrinkPotion();
            potion.Drink(AccessPoint.Player, _activePotionsContainer);
            Destroy(_highlightedItem.gameObject);
        }
        else if (_highlightedItem.Item.TryGetComponent(out GoldenKey key))
        {
            if (key.TryOpenLock(AccessPoint.Player.GetWorldCenter()))
            {
                Destroy(_highlightedItem.gameObject);
                gameObject.SetActive(false);
            }
        }
    }

    private void TakeOffItem(ItemRenderer itemRenderer)
    {
        itemRenderer.transform.SetParent(_itemsContainer);
        itemRenderer.Item.GetComponent<EquippableItem>().TakeOff(AccessPoint.Player);
    }
}