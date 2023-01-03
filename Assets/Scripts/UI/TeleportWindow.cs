using System.Collections.Generic;
using UnityEngine;

public class TeleportWindow : MonoBehaviour
{
    [SerializeField] private List<Statue> _statues;
    [SerializeField] private StatueView _statueViewTemplate;
    [SerializeField] private RectTransform _container;

    private void Start()
    {
        foreach (var statue in _statues)
        {
            var statueView = Instantiate(_statueViewTemplate, _container);
            statueView.Render(statue);
            statueView.ButtonClicked += OnStatueClick;
        }
    }

    private void OnStatueClick(Statue statue)
    {
        Debug.Log("Click");
    }
}
