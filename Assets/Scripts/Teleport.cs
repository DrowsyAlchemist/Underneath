using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleport : MonoBehaviour
{
    [SerializeField] private List<TeleportPoint> _teleportPoints;
    [SerializeField] private RectTransform _teleportWindow;
    [SerializeField] private TeleportPointRenderer _teleportPointRendererTemplate;
    [SerializeField] private RectTransform _container;

    private static Teleport _instance;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        gameObject.SetActive(false);
        Clear();

        foreach (TeleportPoint point in _teleportPoints)
            AddPointRenderer(point);
    }

    public static void ShowWindow()
    {
        Time.timeScale = 0;
        _instance._teleportWindow.gameObject.SetActive(true);

        foreach (TeleportPoint point in _instance._teleportPoints)
        {
            if (point.TargetLocationName.Equals(SceneManager.GetActiveScene().name))
            {
                point.SetAvailable();
                return;
            }
        }
        throw new System.Exception("Teleport point is not found.");
    }

    public static void HideWindow()
    {
        Time.timeScale = 1;
        _instance._teleportWindow.gameObject.SetActive(false);
    }

    private void AddPointRenderer(TeleportPoint point)
    {
        TeleportPointRenderer pointRenderer = Instantiate(_teleportPointRendererTemplate, _container);
        pointRenderer.Render(point);
        pointRenderer.ButtonClicked += OnTeleportPointClick;
    }

    private void Clear()
    {
        foreach (var view in _container.GetComponentsInChildren<TeleportPointRenderer>())
            Destroy(view.gameObject);
    }

    private void OnTeleportPointClick(TeleportPoint teleportPoint)
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
        teleportPoint.Teleport();
    }
}