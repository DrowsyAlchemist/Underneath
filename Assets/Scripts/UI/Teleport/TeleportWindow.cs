using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportWindow : MonoBehaviour
{
    [SerializeField] private List<TeleportPoint> _teleportPoints;
    [SerializeField] private RectTransform _teleportWindow;
    [SerializeField] private TeleportPointRenderer _teleportPointRendererTemplate;
    [SerializeField] private RectTransform _pointsContainer;

    private static TeleportWindow _instance;

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

    private void OnDestroy()
    {
        foreach (var pointRenderer in _pointsContainer.GetComponentsInChildren<TeleportPointRenderer>())
            pointRenderer.ButtonClicked -= OnTeleportPointClick;
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
        TeleportPointRenderer pointRenderer = Instantiate(_teleportPointRendererTemplate, _pointsContainer);
        pointRenderer.Render(point);
        pointRenderer.ButtonClicked += OnTeleportPointClick;
    }

    private void Clear()
    {
        foreach (var view in _pointsContainer.GetComponentsInChildren<TeleportPointRenderer>())
            Destroy(view.gameObject);
    }

    private void OnTeleportPointClick(TeleportPoint teleportPoint)
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
        teleportPoint.Teleport();
    }
}