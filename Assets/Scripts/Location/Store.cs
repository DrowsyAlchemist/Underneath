using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(BrightnessController))]
public class Store : MonoBehaviour
{
    [SerializeField] private StoreMenu _menu;
    [SerializeField] private List<Item> _wares = new List<Item>();

    private BrightnessController _lightControl;

    private void Start()
    {
        _lightControl = GetComponent<BrightnessController>();
        _lightControl.Unlit();
        FillStore();
        enabled = false;
    }

    private void FillStore()
    {
        foreach (var ware in _wares)
            _menu.AddWare(ware);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            enabled = true;
            _lightControl.Lit();
            string message = "Press \"S\" to open store.";
            MessageCreator.ShowMessage(message, AccessPoint.InterfaceCanvas, MessageType.Tip);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            enabled = false;
            _lightControl.Unlit();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (_menu.gameObject.activeSelf)
            {
                _menu.gameObject.SetActive(false);
                Time.timeScale = 1;
            }
            else
            {
                _menu.gameObject.SetActive(true);
                Time.timeScale = 0;
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_menu.gameObject.activeSelf)
            {
                _menu.gameObject.SetActive(false);
                Time.timeScale = 1;
            }
        }
    }

    private void OnValidate()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }
}
