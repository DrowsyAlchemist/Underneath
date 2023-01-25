using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(BrightnessController))]
public class Store : MonoBehaviour
{
    [SerializeField] private StoreMenu _menu;
    [SerializeField] private List<Item> _wareTemplates = new();

    private BrightnessController _brightnessController;

    private void Start()
    {
        _brightnessController = GetComponent<BrightnessController>();
        _brightnessController.Unlit();
        FillStore();
        enabled = false;
    }

    private void FillStore()
    {
        foreach (var ware in _wareTemplates)
            _menu.AddWare(ware);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player _))
        {
            enabled = true;
            _brightnessController.Lit();
            string message = "Press \"S\" to open store.";
            MessageCreator.ShowMessage(message, AccessPoint.InterfaceCanvas, MessageType.Tip);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player _))
        {
            enabled = false;
            _brightnessController.Unlit();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (_menu.gameObject.activeSelf)
                SetMenuActive(false);
            else
                SetMenuActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
            if (_menu.gameObject.activeSelf)
                SetMenuActive(false);
    }

    private void SetMenuActive(bool isActive)
    {
        _menu.gameObject.SetActive(isActive);
        Time.timeScale = isActive ? 0 : 1;
    }

    private void OnValidate()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }
}