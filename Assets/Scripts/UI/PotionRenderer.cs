using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PotionRenderer : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Button _button;

    public Potion Potion { get; private set; }

    public event UnityAction<PotionRenderer> ButtonClicked;

    private void OnEnable()
    {
        _button.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnButtonClick);
    }

    public void Render(Potion potion)
    {
        Potion = potion;
        _image.sprite = potion.Sprite;
    }

    private void OnButtonClick()
    {
        ButtonClicked?.Invoke(this);
    }
}
