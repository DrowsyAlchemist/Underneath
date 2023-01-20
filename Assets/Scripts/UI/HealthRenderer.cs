using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthRenderer : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private RectTransform _heartsView;
    [SerializeField] private RectTransform _emptyHeartsView;

    [SerializeField] private Image _heartTemplate;
    [SerializeField] private Image _emptyHeartTemplate;

    private Health _playerHealth;
    private List<Image> _hearts = new List<Image>();
    private List<Image> _emptyHearts = new List<Image>();

    private void Start()
    {
        _playerHealth = _player.Health;
        _playerHealth.CurrentHealthChanged += OnHealthChanged;
        _playerHealth.MaxHealthChanged += OnMaxHealthChanged;
        OnHealthChanged(_playerHealth.CurrentHealth);
        OnMaxHealthChanged(_playerHealth.MaxHealth);
    }

    private void OnDestroy()
    {
        _playerHealth.CurrentHealthChanged -= OnHealthChanged;
        _playerHealth.MaxHealthChanged -= OnMaxHealthChanged;
    }

    private void OnHealthChanged(int health)
    {
        while (health > _hearts.Count)
            _hearts.Add(Instantiate(_heartTemplate, _heartsView));

        for (int i = 0; i < _hearts.Count; i++)
            _hearts[i].gameObject.SetActive(i < health);
    }

    private void OnMaxHealthChanged(int maxHealth)
    {
        while (maxHealth > _emptyHearts.Count)
            _emptyHearts.Add(Instantiate(_emptyHeartTemplate, _emptyHeartsView));

        for (int i = 0; i < _emptyHearts.Count; i++)
            _emptyHearts[i].gameObject.SetActive(i < maxHealth);
    }
}