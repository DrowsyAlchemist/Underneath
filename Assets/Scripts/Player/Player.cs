using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(PlayerAnimation))]
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerHealth))]
public class Player : MonoBehaviour, ITakeDamage
{
    [SerializeField] private Inventory _inventory;
    [SerializeField] private float _dropForce = 15;
    //[SerializeField] private Transform _shootPoint;

    protected bool Knocked;
    private Collider2D _collider;

   // public Transform ShootPoint => _shootPoint;
    public PlayerMovement PlayerMovement { get; private set; }
    public PlayerAnimation PlayerAnimation { get; private set; }
    public PlayerHealth PlayerHealth { get; private set; }
    public int Money { get; private set; }
    public Inventory Inventory => _inventory;

    public event UnityAction<int> MoneyChanged;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        PlayerAnimation = GetComponent<PlayerAnimation>();
        PlayerMovement = GetComponent<PlayerMovement>();
        PlayerHealth = GetComponent<PlayerHealth>();
    }

    public Vector3 GetWorldCenter()
    {
        if (_collider==null)
            return Vector3.zero;

        Vector3 localCenter = _collider.offset;
        return transform.position + localCenter;
    }

    public void TakeMoney(int money)
    {
        if (money < 0)
            throw new System.ArgumentOutOfRangeException();

        Money += money;
        MoneyChanged?.Invoke(Money);
    }

    public int GiveMoney(int money)
    {
        if (money > Money)
            throw new System.Exception("Not enough money.");

        Money -= money;
        MoneyChanged?.Invoke(Money);
        return money;
    }

    public void TakeDamage(int damage, Vector3 soursePosition)
    {
        if (PlayerHealth.IsInvulnerability == false)
        {
            PlayerHealth.ReduceHealth(damage);
            Vector2 dropForce = _dropForce * (GetWorldCenter() - soursePosition).normalized;
            Debug.DrawRay(transform.position, dropForce, Color.red, 1);
            PlayerMovement.AddForce(dropForce);
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