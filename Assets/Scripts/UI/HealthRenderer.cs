using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthRenderer : MonoBehaviour
{
    [SerializeField] private RectTransform _heartsView;
    [SerializeField] private RectTransform _emptyHeartsView;

    [SerializeField] private Image _heart;
    [SerializeField] private Image _emptyHeart;

    private Health _playerHealth;
    private List<Image> _hearts = new List<Image>();
    private List<Image> _emptyHearts = new List<Image>();

    private void Start()
    {
        _playerHealth = AccessPoint.Player.Health;
        _playerHealth.HealthChanged += OnHealthChanged;
        _playerHealth.MaxHealthChanged += OnMaxHealthChanged;
        OnMaxHealthChanged(_playerHealth.MaxHealth);
        OnHealthChanged(_playerHealth.CurrentHealth);
    }

    private void OnDestroy()
    {
        _playerHealth.HealthChanged -= OnHealthChanged;
        _playerHealth.MaxHealthChanged -= OnMaxHealthChanged;
    }

    private void OnMaxHealthChanged(int maxHealth)
    {
        while (maxHealth > _emptyHearts.Count)
            _emptyHearts.Add(Instantiate(_emptyHeart, _emptyHeartsView));

        for (int i = 0; i < _emptyHearts.Count; i++)
            _emptyHearts[i].gameObject.SetActive(i < maxHealth);
    }

    private void OnHealthChanged(int health)
    {
        while (health > _hearts.Count)
            _hearts.Add(Instantiate(_heart, _heartsView));

        for (int i = 0; i < _hearts.Count; i++)
            _hearts[i].gameObject.SetActive(i < health);
    }
}
