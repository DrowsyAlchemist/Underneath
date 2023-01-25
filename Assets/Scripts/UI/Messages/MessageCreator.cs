using UnityEngine;

public class MessageCreator : MonoBehaviour
{
    [SerializeField] private Message _messsageTemplate;
    [SerializeField] private Tip _tipTemplate;

    private static MessageCreator _instance;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(gameObject);
    }

    public static void ShowMessage(string message, RectTransform parentPanel, MessageType type)
    {
        MessageWindow window = type switch
        {
            MessageType.Message => Instantiate(_instance._messsageTemplate, parentPanel),
            MessageType.Tip => Instantiate(_instance._tipTemplate, parentPanel),
            _ => throw new System.InvalidOperationException("Message type is not implemented."),
        };
        window.Show(message);
    }
}