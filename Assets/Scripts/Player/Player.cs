using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(PlayerAnimation))]
[RequireComponent(typeof(PlayerMovement))]
public class Player : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 2;
    [SerializeField] private float _invulnerabilityDuration = 1;

    private int _currentHealth;
    private int _money;
    private bool _isInvulnerability;

    public int MaxHealth => _maxHealth;
    public PlayerMovement PlayerMovement { get; private set; }
    public PlayerAnimation PlayerAnimation { get; private set; }

    public event UnityAction<int> HealthChanged;
    public event UnityAction<int> MoneyChanged;


    private void Start()
    {
        PlayerAnimation = GetComponent<PlayerAnimation>();
        PlayerMovement = GetComponent<PlayerMovement>();

        _currentHealth = _maxHealth;
        HealthChanged?.Invoke(_currentHealth);
        MoneyChanged?.Invoke(_money);
    }


    public void TakeCoin()
    {
        _money++;
        MoneyChanged?.Invoke(_money);
    }

    public void TakeDamage()
    {
        if (_isInvulnerability == false)
        {
            _currentHealth--;
            HealthChanged?.Invoke(_currentHealth);
            Knock();

            if (_currentHealth > 0)
                StartCoroutine(StandUp());
            else
                Die();
        }
    }

    private void Knock()
    {
        _isInvulnerability = true;
        SetMoverActive(false);
        PlayerAnimation.PlayKnock();
    }

    private IEnumerator StandUp()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForSeconds(PlayerAnimation.Animator.GetCurrentAnimatorStateInfo(0).length);
        PlayerAnimation.PlayStandUp();
        yield return new WaitForEndOfFrame();
        yield return new WaitForSeconds(PlayerAnimation.Animator.GetCurrentAnimatorStateInfo(0).length);
        SetMoverActive(true);
        PlayerAnimation.PlayInvulnerability();
        StartCoroutine(ResetInvulnerability());
    }

    private IEnumerator ResetInvulnerability()
    {
        yield return new WaitForSeconds(_invulnerabilityDuration);
        _isInvulnerability = false;
        PlayerAnimation.StopInvulnerability();
    }

    private void Die()
    {

    }

    private void SetMoverActive(bool isActive)
    {
        PlayerMovement.AllowAnimation(isActive);
        PlayerMovement.AllowInpupControl(isActive);
    }
}