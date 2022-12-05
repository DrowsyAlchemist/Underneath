using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class WareRenderer : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _lable;
    [SerializeField] private TMP_Text _description;
    [SerializeField] private TMP_Text _cost;
    [SerializeField] private Button _buyButton;

    public UseableItem Item { get; private set; }
    public int Cost => int.Parse(_cost.text);

    public event UnityAction<WareRenderer> ButtonClicked;

    private void OnEnable()
    {
        _buyButton.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _buyButton.onClick.RemoveListener(OnButtonClick);
    }

    public void SetCost(int cost)
    {
        _cost.text = cost.ToString();
    }

    public void Render(UseableItem item)
    {
        Item = item;
        _image.sprite = item.Sprite;
        _lable.text = item.Lable;
        _description.text = item.Description;
        _cost.text = item.Cost.ToString();
    }

    private void OnButtonClick()
    {
        ButtonClicked?.Invoke(this);
    }
}
