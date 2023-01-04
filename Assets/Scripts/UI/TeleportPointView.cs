using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TeleportPointView : MonoBehaviour
{
    [SerializeField] private Image _discoveredFrame;
    [SerializeField] private TMP_Text _lable;
    [SerializeField] private Button _button;

    private TeleportPoint _teleportPoint;

    public event UnityAction<TeleportPoint> ButtonClicked;

    public void Render(TeleportPoint point)
    {
        _teleportPoint = point;
        _lable.text = point.TargetLocationName;
        _button.interactable = point.IsDiscovered;
        _discoveredFrame.gameObject.SetActive(point.IsDiscovered);
    }

    private void Start()
    {
        _button.onClick.AddListener(OnButtonClick);
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        ButtonClicked?.Invoke(_teleportPoint);
    }
}
