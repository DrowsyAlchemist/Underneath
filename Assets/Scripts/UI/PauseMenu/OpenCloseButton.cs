using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class OpenCloseButton : UIButton
{
    [SerializeField] private RectTransform _panel;
    [SerializeField] private bool _canOpen = true;
    [SerializeField] private bool _canClose;

    protected override void OnButtonClick()
    {
        if (_panel.gameObject.activeSelf)
        {
            if (_canClose)
                Close();
        }
        else if (_canOpen)
        {
            Open();
        }
    }

    protected virtual void Open()
    {
        _panel.gameObject.SetActive(true);
    }

    protected virtual void Close()
    {
        _panel.gameObject.SetActive(false);
    }
}
