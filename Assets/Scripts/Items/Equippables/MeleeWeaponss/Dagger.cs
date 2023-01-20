using UnityEngine;

public class Dagger : AffectingItem
{
    [SerializeField] private int _damage;
    [SerializeField] private float _knifeAttackRange;
    [SerializeField] private float _secondsBetweenKnifeAttacks;
    [SerializeField] private ParticleSystem _knifeAttackEffect;
    [SerializeField] private AudioSource _swingSound;

    private Rigidbody2D _playerBody;
    private float _timeAfterKnifeAttack;

    public bool CanAttack => (_timeAfterKnifeAttack > _secondsBetweenKnifeAttacks);

    public void Init(Player player)
    {
        transform.SetParent(player.transform);
        _playerBody = player.GetComponent<Rigidbody2D>();
        _knifeAttackEffect = Instantiate(_knifeAttackEffect, transform);
        SetEffectPosition(player.transform);
    }

    private void OnEnable()
    {
        _timeAfterKnifeAttack = _secondsBetweenKnifeAttacks;
    }

    private void Update()
    {
        _timeAfterKnifeAttack += Time.deltaTime;
    }

    public void IncreaseDamage(int value)
    {
        _damage += value;
    }

    public void DecreaseDamage(int value)
    {
        _damage -= value;
    }

    public void ModifyAttackRange(float modifier)
    {
        _knifeAttackRange *= modifier;
        SetEffectPosition(_playerBody.transform);
    }

    public void Attack()
    {
        if (CanAttack)
        {
            _swingSound.Play();
            _knifeAttackEffect.Play();
            _timeAfterKnifeAttack = 0;

            Vector2 direction = (_playerBody.transform.localScale.x > 0) ? Vector2.right : Vector2.left;
            RaycastHit2D[] hits = new RaycastHit2D[8];
            int hitCount = _playerBody.Cast(direction, hits, _knifeAttackRange);

            for (int i = 0; i < hitCount; i++)
                if (hits[i].transform.TryGetComponent(out ITakeDamage target))
                    target.TakeDamage(_damage, transform.position);
        }
    }

    public void SetEffectPosition(Transform playerTransform)
    {
        Vector3 direction = (playerTransform.localScale.x > 0) ? Vector2.right : Vector2.left;
        _knifeAttackEffect.transform.position = playerTransform.position + _knifeAttackRange * direction;
    }

    protected override void StartAffecting(Player player)
    {
        Init(player);
        player.Inventory.SetDagger(this);
    }

    protected override void StopAffecting(Player player)
    {
        player.Inventory.TakeOffDagger();
    }
}