using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ItemRenderer : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Image _highlightedFrame;
    [SerializeField] private Button _button;

    public Item Item { get; private set; }

    public event UnityAction<ItemRenderer> ButtonClicked;

    public void SetHighlighted(bool isHighlighted)
    {
        _highlightedFrame.gameObject.SetActive(isHighlighted);
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnButtonClick);
    }

    public void Render(Item item)
    {
        Item = item;
        _image.sprite = item.Data.Sprite;
    }

    private void OnButtonClick()
    {
        ButtonClicked?.Invoke(this);
    }
}