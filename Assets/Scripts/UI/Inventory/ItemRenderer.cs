using System.Collections;
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
        if (item is Dagger || item is Gun)
            Item = Instantiate(item, transform);
        else
            Item = item;

        _image.sprite = item.Data.Sprite;
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
