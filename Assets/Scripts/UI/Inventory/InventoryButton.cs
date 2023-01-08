using UnityEngine;
using UnityEngine.UI;

public class InventoryButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private GameObject _inventoryPanel;

    private void OnEnable()
    {
        _button.onClick.AddListener(OpenInventoryPanel);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OpenInventoryPanel);
    }

    private void Start()
    {
        _inventoryPanel.SetActive(false);
    }

    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.I))
            if (_inventoryPanel.activeSelf == false)
                OpenInventoryPanel();

        if (_inventoryPanel.activeSelf)
            if (Input.GetKeyDown(KeyCode.Escape))
                CloseInventoryPanel();
    }

    private void OpenInventoryPanel()
    {
        _inventoryPanel.SetActive(true);
        Time.timeScale = 0;
    }

    private void CloseInventoryPanel()
    {
        _inventoryPanel.SetActive(false);
        Time.timeScale = 1;
    }
}
