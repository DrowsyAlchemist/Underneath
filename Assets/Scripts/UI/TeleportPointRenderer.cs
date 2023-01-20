using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TeleportPointRenderer : MonoBehaviour
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
        _button.interactable = point.IsAvailable;

        if (point.IsAvailable)
            OnPointBecameAvailable(point);
        else
            point.BecameAvailable += OnPointBecameAvailable;
    }

    private void OnPointBecameAvailable(TeleportPoint point)
    {
        _button.interactable = point.IsAvailable;
        _discoveredFrame.gameObject.SetActive(point.IsAvailable);
        point.BecameAvailable -= OnPointBecameAvailable;
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