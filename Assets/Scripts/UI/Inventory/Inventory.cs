using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] private AccessPoint _accessPoint;

    [SerializeField] private RectTransform _itemsContainer;
    [SerializeField] private ItemRenderer _itemRenderer;

    [SerializeField] private ItemDescription _itemDescription;

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
        _useButton.onClick.AddListener(UseItem);
    }

    private void Start()
    {
        foreach (var item in _defaultItems)
            AddItem(item);
    }

    private void OnDisable()
    {
        _useButton.onClick.RemoveListener(UseItem);

        if (_highlightedItem != null)
            _highlightedItem.SetHighlighted(false);
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
        _itemDescription.Render(itemRenderer.Item);
        _useButton.interactable = true;
    }

    private void UseItem()
    {
        _highlightedItem.SetHighlighted(false);
        _itemDescription.Clear();
        _useButton.interactable = false;

        if (_highlightedItem.Item.TryGetComponent(out EquippableItem equippableItem))
            EquipItem(equippableItem);
        else if (_highlightedItem.Item.TryGetComponent(out Potion potion))
            UsePotion(potion);
        else if (_highlightedItem.Item.TryGetComponent(out GoldenKey key))
            UseKey(key);
    }

    private void EquipItem(EquippableItem equippableItem)
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

    private void UsePotion(Potion potion)
    {
        UISounds.PlayDrinkPotion();
        potion.Drink(AccessPoint.Player, _activePotionsContainer);
        Destroy(_highlightedItem.gameObject);
    }

    private void UseKey(GoldenKey key)
    {
        if (key.TryOpenLock(AccessPoint.Player.GetWorldCenter()))
        {
            Destroy(_highlightedItem.gameObject);
            gameObject.SetActive(false);
        }
    }

    private void TakeOffItem(ItemRenderer itemRenderer)
    {
        itemRenderer.transform.SetParent(_itemsContainer);
        var equippableItem = itemRenderer.Item.GetComponent<EquippableItem>();
        equippableItem.TakeOff(AccessPoint.Player);
    }


}