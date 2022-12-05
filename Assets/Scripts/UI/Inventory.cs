using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] private AccessPoint _game;

    [SerializeField] private RectTransform _itemsContainer;
    [SerializeField] private UseableItemRenderer _itemRenderer;

    [SerializeField] private Image _descriptionImage;
    [SerializeField] private TMP_Text _lable;
    [SerializeField] private TMP_Text _description;
    [SerializeField] private Button _useButton;

    [SerializeField] private RectTransform _activeEfectsContainer;

    private List<UseableItem> _items = new List<UseableItem>();
    private UseableItemRenderer _highlightedItem;

    private void OnEnable()
    {
        _useButton.onClick.AddListener(OnUseButtonClick);
    }

    private void OnDisable()
    {
        _useButton.onClick.RemoveListener(OnUseButtonClick);
    }

    private void Start()
    {
        ClearDescription();
    }

    public void AddItem(UseableItem item)
    {
        _items.Add(item);
        var itemRenderer = Instantiate(_itemRenderer, _itemsContainer);
        itemRenderer.Render(item);
        itemRenderer.ButtonClicked += OnItemClick;
    }

    private void OnItemClick(UseableItemRenderer itemRenderer)
    {
        _highlightedItem = itemRenderer;
        RenderDescription(itemRenderer.Item);
    }

    private void RenderDescription(UseableItem item)
    {
        _descriptionImage.sprite = item.Sprite;
        _descriptionImage.color = Color.white;
        _lable.text = item.Lable;
        _description.text = item.Description;
        _useButton.interactable = item.TryGetComponent(out UseableItem _);
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
        if (_highlightedItem.Item.TryGetComponent(out UseableItem item))
        {
            item.Use(_game);
            ClearDescription();

            if (_highlightedItem.Item.TryGetComponent(out Potion potion))
            {
                _highlightedItem.transform.SetParent(_activeEfectsContainer);
                _highlightedItem.VanishWithDelay(potion.Duration);
            }
            else if (_highlightedItem.Item.TryGetComponent(out PermanentEffectItem _))
            {
                _highlightedItem.transform.SetParent(_activeEfectsContainer);
            }
            else
            {
                Destroy(_highlightedItem.gameObject);
            }
        }
    }
}