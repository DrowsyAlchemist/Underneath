using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 4;

    public int MaxHealth => _maxHealth;
    public int CurrentHealth { get; private set; }

    public event UnityAction<int> MaxHealthChanged;
    public event UnityAction<int> HealthChanged;

    private void Awake()
    {
        CurrentHealth = _maxHealth;
    }

    public void IncreaseMaxHealth(int value)
    {
        if (value < 0)
            throw new System.ArgumentOutOfRangeException("value");

        SetMaxHealth(_maxHealth + value);
        RestoreHealth(value);
    }

    public void DecreaseMaxHealth(int value)
    {
        SetMaxHealth(_maxHealth - value);

        if (CurrentHealth > _maxHealth)
            SetHealth(_maxHealth);
    }

    public void ReduceHealth(int value)
    {
        if (value < 0)
            throw new System.ArgumentOutOfRangeException("value");

        SetHealth(CurrentHealth - value);
    }

    public void RestoreHealth(int value)
    {
        if (value < 0)
            throw new System.ArgumentOutOfRangeException("value");

        SetHealth(CurrentHealth + value);
    }

    private void SetMaxHealth(int maxHealth)
    {
        _maxHealth = maxHealth;

        if (_maxHealth < 1)
            _maxHealth = 1;

        MaxHealthChanged?.Invoke(_maxHealth);
    }

    private void SetHealth(int health)
    {
        CurrentHealth = Mathf.Clamp(health, 0, _maxHealth);
        HealthChanged?.Invoke(CurrentHealth);
    }
}