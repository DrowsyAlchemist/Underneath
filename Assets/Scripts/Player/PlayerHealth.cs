using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(PlayerHurtEffect))]
public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 4;
    [SerializeField] private float _invulnerabilityDuration = 2;

    private Player _player;
    private PlayerHurtEffect _hurtEffect;

    public int MaxHealth => _maxHealth;
    public int CurrentHealth { get; private set; }
    public bool IsInvulnerability { get; private set; }

    public event UnityAction<int> MaxHealthChanged;
    public event UnityAction<int> HealthChanged;

    private void Start()
    {
        _player = GetComponent<Player>();
        _hurtEffect = GetComponent<PlayerHurtEffect>();

        CurrentHealth = _maxHealth;
        MaxHealthChanged?.Invoke(_maxHealth);
        HealthChanged?.Invoke(CurrentHealth);
    }

    public void IncreaseMaxHealth(int value)
    {
        if (value < 0)
            throw new System.ArgumentOutOfRangeException("value");

        SetMaxHealth(_maxHealth + value);
    }

    public void DecreaseMaxHealth(int value)
    {
        if (value < 0)
            throw new System.ArgumentOutOfRangeException("value");

        SetMaxHealth(_maxHealth - value);
    }


    public void ReduceHealth(int value)
    {
        if (value < 0)
            throw new System.ArgumentOutOfRangeException("value");

        if (IsInvulnerability == false)
        {
            IsInvulnerability = true;
            SetHealth(CurrentHealth - value);
            _hurtEffect.Play();
            StartCoroutine(PlayInvulnerability());
        }
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

    private IEnumerator PlayInvulnerability()
    {
        _player.PlayerAnimation.PlayInvulnerability();
        yield return new WaitForSeconds(_invulnerabilityDuration);
        IsInvulnerability = false;
        _player.PlayerAnimation.StopInvulnerability();
    }
}