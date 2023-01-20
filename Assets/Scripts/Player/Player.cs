using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(AdventureGirlAnimation))]
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(Health))]
public class Player : MonoBehaviour, ITakeDamage, ISaveable
{
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private float _dropForce = 15;

    [SerializeField] private int _initialHealth = 4;
    [SerializeField] private float _invulnerabilityDuration = 2;
    [SerializeField] private PlayerHurtEffect _hurtEffect;

    private const string SavesFolderName = "Player";
    private const string PositionFileName = "Position";
    private static Player _instance;
    private Collider2D _collider;
    private bool _knocked;
    private bool _isInvulnerability;

    public PlayerMovement PlayerMovement { get; private set; }
    public AdventureGirlAnimation PlayerAnimation { get; private set; }
    public Health Health { get; private set; }
    public Inventory Inventory { get; private set; }
    public Wallet Wallet { get; private set; }

    public void ResetPlayer()
    {
        Health = Health.LoadLastSaveOrDefault(_initialHealth);
        Wallet = Wallet.GetLastSaveOrDefault();

        if (Inventory != null)
        {
            if (Inventory.Gun != null)
                Destroy(Inventory.Gun.gameObject);

            if (Inventory.Dagger != null)
                Destroy(Inventory.Dagger.gameObject);
        }
        Inventory = Inventory.LoadLastSaveOrDefault(this);
    }

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            _collider = GetComponent<Collider2D>();
            PlayerAnimation = GetComponent<AdventureGirlAnimation>();
            PlayerMovement = GetComponent<PlayerMovement>();
        }
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

    public string GetSavedSceneName()
    {
        var position = SaveLoadManager.GetLoadOrDefault<PlayerPosition>(SavesFolderName, PositionFileName);

        if (position == null)
            return default;
        else
            return position.SceneName;
    }

    public Vector3 GetSavedPosition()
    {
        var position = SaveLoadManager.GetLoadOrDefault<PlayerPosition>(SavesFolderName, PositionFileName);

        if (position == null)
            return default;
        else
            return new Vector3(position.X, position.Y, position.Z);
    }

    public void Save()
    {
        Wallet.Save();
        Health.Save();
        Inventory.Save();

        string sceneName = SceneManager.GetActiveScene().name;
        var savePoints = FindObjectsOfType<SavePoint>();

        if (savePoints == null)
            throw new System.Exception($"Can't find TeleportArea on scene \"{sceneName}\"");

        float minDistanse = float.MaxValue;
        SavePoint nearestPoint = null;

        foreach (var savePoint in savePoints)
        {
            float distance = Vector2.Distance(transform.position, savePoint.SpawnPoint);

            if (distance < minDistanse)
            {
                minDistanse = distance;
                nearestPoint = savePoint;
            }
        }
        Vector3 spawnPosition = nearestPoint.SpawnPoint;

        var playerPosition = new PlayerPosition(sceneName, spawnPosition);
        SaveLoadManager.Save(SavesFolderName, PositionFileName, playerPosition);
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

    [System.Serializable]
    private class PlayerPosition
    {
        public string SceneName;
        public float X;
        public float Y;
        public float Z;

        public PlayerPosition(string sceneName, Vector3 vector)
        {
            SceneName = sceneName;
            X = vector.x;
            Y = vector.y;
            Z = vector.z;
        }
    }
}