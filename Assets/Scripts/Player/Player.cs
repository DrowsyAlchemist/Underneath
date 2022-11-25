using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(PlayerAnimation))]
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerHealth))]
public class Player : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;

    protected bool Knocked;
    private int _money;
    private Collider2D _collider;

    public PlayerMovement PlayerMovement { get; private set; }
    public PlayerAnimation PlayerAnimation { get; private set; }
    public PlayerHealth PlayerHealth { get; private set; }
    public Inventory Inventory => _inventory;
    public int Money => _money;

    public event UnityAction<int> MoneyChanged;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        PlayerAnimation = GetComponent<PlayerAnimation>();
        PlayerMovement = GetComponent<PlayerMovement>();
        PlayerHealth = GetComponent<PlayerHealth>();
    }

    private void Start()
    {
        MoneyChanged?.Invoke(_money);
    }

    public Vector3 GetWorldCenter()
    {
        Vector3 localCenter = _collider.offset;
        return transform.position + localCenter;
    }

    public void TakeMoney(int money)
    {
        if (money < 0)
            throw new System.ArgumentOutOfRangeException();

        _money += money;
        MoneyChanged?.Invoke(_money);
    }

    public int GiveMoney(int money)
    {
        if (money > _money)
            throw new System.Exception("Not enough money.");

        _money -= money;
        MoneyChanged?.Invoke(_money);
        return money;
    }

    public void TakeDamage(int damage)
    {
        if (PlayerHealth.IsInvulnerability == false)
        {
            PlayerHealth.ReduceHealth(damage);
            Knock();

            if (PlayerHealth.CurrentHealth > 0)
                StartCoroutine(StandUp());
            else
                Die();
        }
    }

    private void Knock()
    {
        Knocked = true;
        PlayerMovement.AllowAnimation(false);
        PlayerMovement.AllowInpupControl(false);
        PlayerAnimation.PlayKnock();
    }

    private IEnumerator StandUp()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForSeconds(PlayerAnimation.Animator.GetCurrentAnimatorStateInfo(0).length);
        PlayerAnimation.PlayStandUp();
        yield return new WaitForEndOfFrame();
        yield return new WaitForSeconds(PlayerAnimation.Animator.GetCurrentAnimatorStateInfo(0).length);
        PlayerMovement.AllowAnimation(true);
        PlayerMovement.AllowInpupControl(true);
        Knocked = false;
    }

    private void Die()
    {

    }
}