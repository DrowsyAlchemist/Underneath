using System;
using UnityEngine;

public class Health
{
    private const string SavesFolderName = "Player";
    private const string MaxHealthFileName = "MaxHealth";
    private const string CurrentHealthFileName = "CurrentHealth";

    private static Health _instance;
    private int _maxHealth = 4;

    public int MaxHealth => _maxHealth;
    public int CurrentHealth { get; private set; }

    public event Action<int> MaxHealthChanged;
    public event Action<int> CurrentHealthChanged;

    private Health()
    {

    }

    public static Health LoadLastSaveOrDefault(int defafultHealth)
    {
        if (defafultHealth <= 0)
            throw new ArgumentOutOfRangeException("maxHealth");

        if (_instance == null)
            _instance = new Health();

        _instance.LoadOrSetDefault(defafultHealth);
        return _instance;
    }

    private void LoadOrSetDefault(int maxHealth)
    {
        _maxHealth = SaveLoadManager.GetLoadOrDefault<int>(SavesFolderName, MaxHealthFileName);
        CurrentHealth = SaveLoadManager.GetLoadOrDefault<int>(SavesFolderName, CurrentHealthFileName);

        if (_maxHealth == default)
        {
            _maxHealth = maxHealth;
            CurrentHealth = maxHealth;
        }
        else
        {
            CurrentHealth = Mathf.Clamp(CurrentHealth, 0, _maxHealth);

            if (CurrentHealth == default)
                CurrentHealth = _maxHealth;
        }
        MaxHealthChanged?.Invoke(_maxHealth);
        CurrentHealthChanged?.Invoke(CurrentHealth);
    }

    public void Save()
    {
        SaveLoadManager.Save(SavesFolderName, MaxHealthFileName, _maxHealth);
        SaveLoadManager.Save(SavesFolderName, CurrentHealthFileName, CurrentHealth);
    }

    public void IncreaseMaxHealth(int value)
    {
        if (value < 0)
            throw new ArgumentOutOfRangeException("value");

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
            throw new ArgumentOutOfRangeException("value");

        SetHealth(CurrentHealth - value);
    }

    public void RestoreHealth(int value)
    {
        if (value < 0)
            throw new ArgumentOutOfRangeException("value");

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
        CurrentHealthChanged?.Invoke(CurrentHealth);
    }
}