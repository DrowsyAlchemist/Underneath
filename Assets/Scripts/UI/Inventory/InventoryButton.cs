using UnityEngine;
using UnityEngine.UI;

public class InventoryButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private GameObject _inventoryPanel;

    private void OnEnable()
    {
        _button.onClick.AddListener(OnInventoryButtonClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnInventoryButtonClick);
    }

    private void Start()
    {
        _inventoryPanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
            OnInventoryButtonClick();

        if (_inventoryPanel.activeSelf)
            if (Input.GetKeyDown(KeyCode.Escape))
                OnInventoryButtonClick();
    }

    private void OnInventoryButtonClick()
    {
        _inventoryPanel.SetActive(_inventoryPanel.activeSelf == false);
    }
}
