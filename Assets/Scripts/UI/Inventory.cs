using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] private Game _game;

    [SerializeField] private RectTransform _potionsContainer;
    [SerializeField] private PotionRenderer _potionRenderer;

    [SerializeField] private Image _descriptionImage;
    [SerializeField] private TMP_Text _lable;
    [SerializeField] private TMP_Text _description;
    [SerializeField] private Button _useButton;

    private List<Potion> _potions = new List<Potion>();
    private PotionRenderer _highlightedPotion;

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

    public void AddPotion(Potion potion)
    {
        _potions.Add(potion);
        var potionRenderer = Instantiate(_potionRenderer, _potionsContainer);
        potionRenderer.Render(potion);
        potionRenderer.ButtonClicked += OnItemClick;
    }

    private void OnItemClick(PotionRenderer potionRenderer)
    {
        _highlightedPotion = potionRenderer;
        RenderDescription(potionRenderer.Potion);
    }

    private void RenderDescription(Potion potion)
    {
        _descriptionImage.sprite = potion.Sprite;
        _descriptionImage.color = Color.white;
        _lable.text = potion.Lable;
        _description.text = potion.Description;
        _useButton.interactable = potion.TryGetComponent(out IUseable _);
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
        if (_highlightedPotion.Potion.TryGetComponent(out IUseable useable))
        {
            useable.Use(_game.Player);
            Destroy(_highlightedPotion.gameObject);
            ClearDescription();
        }
    }
}