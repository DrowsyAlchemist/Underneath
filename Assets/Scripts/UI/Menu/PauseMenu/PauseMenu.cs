using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private RectTransform _pauseMenuPanel;

    private void Awake()
    {
        _pauseMenuPanel.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_pauseMenuPanel.gameObject.activeSelf)
            {
                _pauseMenuPanel.gameObject.SetActive(false);
                Time.timeScale = 1;
            }
            else if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
                _pauseMenuPanel.gameObject.SetActive(true);
            }
        }
    }
}