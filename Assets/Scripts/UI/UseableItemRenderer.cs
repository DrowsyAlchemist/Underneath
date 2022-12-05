using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UseableItemRenderer : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Button _button;

    public UseableItem Item { get; private set; }

    public event UnityAction<UseableItemRenderer> ButtonClicked;

    private void OnEnable()
    {
        _button.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnButtonClick);
    }

    public void Render(UseableItem potion)
    {
        Item = potion;
        _image.sprite = potion.Sprite;
    }

    public void VanishWithDelay(float delay)
    {
        StartCoroutine(DestroyWithDelay(delay));
    }

    private IEnumerator DestroyWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }

    private void OnButtonClick()
    {
        ButtonClicked?.Invoke(this);
    }
}
