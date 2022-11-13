using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthRenderer : MonoBehaviour
{
    [SerializeField] private Player _player;

    [SerializeField] private RectTransform _heartsView;
    [SerializeField] private RectTransform _emptyHeartsView;

    [SerializeField] private Image _heart;
    [SerializeField] private Image _emptyHeart;

    private List<Image> _hearts = new List<Image>();

    private void OnEnable()
    {
        _player.HealthChanged += OnHealthChanged;
    }

    private void OnDisable()
    {
        _player.HealthChanged -= OnHealthChanged;
    }

    private void Start()
    {
        for (int i = 0; i < _player.MaxHealth; i++)
        {
            Image heart = Instantiate(_heart, _heartsView);
            heart.enabled = true;
            _hearts.Add(heart);

            Instantiate(_emptyHeart, _emptyHeartsView);
        }
    }

    private void OnHealthChanged(int health)
    {
        for (int i = 0; i < _hearts.Count; i++)
            _hearts[i].gameObject.SetActive(i < health);
    }
}
