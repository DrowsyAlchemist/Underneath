using UnityEngine;
using UnityEngine.UI;

public class ResumeButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private RectTransform _pauseMenuPanel;

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
        _pauseMenuPanel.gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
