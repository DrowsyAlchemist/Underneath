using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(LightControl))]
public class Store : MonoBehaviour
{
    [SerializeField] private StoreMenu _menu;
    [SerializeField] private List<Potion> _wares = new List<Potion>();

    private LightControl _lightControl;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            _menu.Init(player);
            enabled = true;
            _lightControl.Lit();
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

    private void Start()
    {
        _lightControl = GetComponent<LightControl>();
        _lightControl.Unlit();
        FillStore();
        enabled = false;
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

    private void FillStore()
    {
        foreach (var ware in _wares)
            _menu.AddWare(ware);
    }

    private void OnValidate()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }
}
