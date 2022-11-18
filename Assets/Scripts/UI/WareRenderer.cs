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

    public Potion Potion { get; private set; }
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

    public void Render(Potion potion)
    {
        Potion = potion;
        _image.sprite = potion.Sprite;
        _lable.text = potion.Lable;
        _description.text = potion.Description;
        _cost.text = potion.Cost.ToString();
    }

    private void OnButtonClick()
    {
        ButtonClicked?.Invoke(this);
    }
}
