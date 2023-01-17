using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventioryWindow : MonoBehaviour, ISaveable
{
    [SerializeField] private ItemRenderer _itemRenderer;

    [SerializeField] private RectTransform _itemsContainer;
    [SerializeField] private ActivePotionsView _activePotionsView;
    [SerializeField] private PlayerSlots _playerSlots;

    [SerializeField] private ItemDescriptionRenderer _itemDescription;
    [SerializeField] private Button _useButton;

    private const string SavesFolder = "Inventory";
    private const string SaveFileName = "Items";
    private const string ItemsFolder = "Items";
    private ItemRenderer _highlightedItem;
    private List<ItemRenderer> _itemRenderers = new List<ItemRenderer>();

    public Inventory Inventory { get; private set; }

    public void Save()
    {
        Inventory.Save();
    }

    public void ResetInventory()
    {
        if (_itemRenderers.Count > 0)
        {
            foreach (var itemRenderer in _itemRenderers)
                Destroy(itemRenderer.gameObject);

            _itemRenderers.Clear();
        }
    }

    private void Load()
    {
        ResetInventory();
        var itemFileNames = SaveLoadManager.GetLoadOrDefault<List<string>>(SavesFolder, SaveFileName);

        if (itemFileNames == null)
            return;

        foreach (var fileName in itemFileNames)
        {
            string localPath = ItemsFolder + "/" + fileName;
            Item item = Resources.Load<Item>(localPath);

            if (item == null)
                throw new System.Exception($"Can't find valid item on path: " + localPath);

            Inventory.AddItem(item);
        }
    }

    private void Start()
    {
        Inventory = Inventory.GetInventory(AccessPoint.Player);
        Inventory.ItemAdded += AddItem;
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        Inventory.ItemAdded -= AddItem;

        for (int i = 0; i < _itemsContainer.childCount; i++)
            _itemsContainer.GetChild(i).GetComponent<ItemRenderer>().ButtonClicked -= OnItemClick;
    }

    private void OnEnable()
    {
        if (Inventory != null && Inventory.IsEmpty)
            Load();

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
        _itemRenderers.Add(itemRenderer);
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
            case AffectingItem:
                SetAffectingItem(_highlightedItem);
                break;
            case GoldenKey:
                Inventory.KeyUsed += OnKeyUsed;
                break;
        }
        Inventory.UseItem(_highlightedItem.Item);
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
                Inventory.UseItem(equippedItemRenderer.Item);
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
            Inventory.KeyUsed -= OnKeyUsed;

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