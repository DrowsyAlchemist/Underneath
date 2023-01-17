using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(AdventureGirlAnimation))]
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(Health))]
public class Player : MonoBehaviour, ITakeDamage, ISaveable
{
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private InventioryWindow _inventoryView;
    [SerializeField] private float _dropForce = 15;

    [SerializeField] private int _initialHealth = 4;
    [SerializeField] private float _invulnerabilityDuration = 2;
    [SerializeField] private PlayerHurtEffect _hurtEffect;

    private Collider2D _collider;
    private bool _knocked;
    private bool _isInvulnerability;

    public PlayerMovement PlayerMovement { get; private set; }
    public AdventureGirlAnimation PlayerAnimation { get; private set; }
    public Health Health { get; private set; }
    public Inventory Inventory => _inventoryView.Inventory;
    public Wallet Wallet { get; private set; }

    public void ResetPlayer()
    {
        Health = Health.GetLoadOrDefault(_initialHealth);
        Wallet = Wallet.GetLoadOrDefault();
        _inventoryView.ResetInventory();
    }

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        PlayerAnimation = GetComponent<AdventureGirlAnimation>();
        PlayerMovement = GetComponent<PlayerMovement>();
        ResetPlayer();
    }

    private void Update()
    {
        if (_knocked == false)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
                KnifeAttack();
            else if (Input.GetKeyDown(KeyCode.Mouse1))
                Shoot();
        }
    }

    public void Save()
    {
        Wallet.Save();
        Health.Save();
    }

    public Vector3 GetPosition()
    {
        Vector3 localCenter = _collider.offset;
        return transform.position + localCenter;
    }

    public void TakeDamage(int damage, Vector3 soursePosition)
    {
        if (damage < 0)
            throw new System.ArgumentOutOfRangeException();

        if (_isInvulnerability == false)
        {
            _isInvulnerability = true;
            StartCoroutine(PlayInvulnerability());

            _hurtEffect.Play();
            Health.ReduceHealth(damage);

            Vector2 dropForce = _dropForce * (GetPosition() - soursePosition).normalized;
            Debug.DrawRay(transform.position, dropForce, Color.red, 1);
            PlayerMovement.AddForce(dropForce);

            Knock();

            if (Health.CurrentHealth > 0)
                StartCoroutine(StandUp());
            else
                Die();
        }
    }

    public void ModifyInvulnerabilityDuration(float modifier)
    {
        if (modifier <= 0)
            throw new System.ArgumentOutOfRangeException("modifier");

        _invulnerabilityDuration *= modifier;
    }

    private void Knock()
    {
        _knocked = true;
        PlayerMovement.AllowAnimation(false);
        PlayerMovement.AllowInpupControl(false);
        PlayerAnimation.PlayKnock();
        PlayerSounds.PlayHurt();
    }

    private IEnumerator StandUp()
    {
        StartCoroutine(PlayInvulnerability());
        yield return new WaitForEndOfFrame();
        yield return new WaitForSeconds(PlayerAnimation.Animator.GetCurrentAnimatorStateInfo(0).length);
        PlayerAnimation.PlayStandUp();
        yield return new WaitForEndOfFrame();
        yield return new WaitForSeconds(PlayerAnimation.Animator.GetCurrentAnimatorStateInfo(0).length);
        PlayerMovement.AllowAnimation(true);
        PlayerMovement.AllowInpupControl(true);
        _knocked = false;
    }

    private IEnumerator PlayInvulnerability()
    {
        PlayerAnimation.PlayInvulnerability();
        yield return new WaitForSeconds(_invulnerabilityDuration);
        _isInvulnerability = false;
        PlayerAnimation.StopInvulnerability();
    }

    private void KnifeAttack()
    {
        if (Inventory.Dagger != null && Inventory.Dagger.CanAttack)
        {
            PlayerAnimation.PlayMelee();
            Inventory.Dagger.Attack();
        }
    }

    private void Shoot()
    {
        if (Inventory.Gun != null && Inventory.Gun.CanShoot)
        {
            PlayerAnimation.PlayShoot();
            var direction = transform.localScale.x * Vector2.right;
            Inventory.Gun.Shoot(_shootPoint.position, direction);
        }
    }

    private void Die()
    {

    }
}