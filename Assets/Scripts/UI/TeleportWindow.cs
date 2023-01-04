using System.Collections.Generic;
using UnityEngine;

public class TeleportWindow : MonoBehaviour
{
    [SerializeField] private TeleportPointView _teleportViewTemplate;
    [SerializeField] private RectTransform _container;
    [SerializeField] private List<TeleportPoint> _teleportPoints;

    private void OnEnable()
    {
        Clear();

        foreach (var point in _teleportPoints)
        {
            Debug.Log(point.TargetLocationName + " : " + PlayerPrefs.GetInt(point.TargetLocationName));
            TeleportPointView teleportView = Instantiate(_teleportViewTemplate, _container);
            teleportView.Render(point);
            teleportView.ButtonClicked += OnStatueClick;
        }
    }

    private void Clear()
    {
        foreach (var view in _container.GetComponentsInChildren<TeleportPointView>())
            Destroy(view.gameObject);
    }

    private void OnStatueClick(TeleportPoint teleportPoint)
    {
        teleportPoint.Teleport();
    }
}
