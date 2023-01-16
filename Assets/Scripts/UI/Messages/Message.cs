using UnityEngine;
using UnityEngine.UI;

public class Message : MessageWindow
{
    [SerializeField] private Button _closeButton;


    private void Start()
    {
        _closeButton.onClick.AddListener(OnCloseButtonClick);
    }

    private void OnCloseButtonClick()
    {
        _closeButton.onClick.RemoveListener(OnCloseButtonClick);
        base.Close(0);
    }
}