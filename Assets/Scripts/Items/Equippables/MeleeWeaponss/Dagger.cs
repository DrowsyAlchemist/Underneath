using UnityEngine;

public class Dagger : EquippableItem
{
    [SerializeField] private int _damage;
    [SerializeField] private float _knifeAttackRange;
    [SerializeField] private float _secondsBetweenKnifeAttacks;
    [SerializeField] private ParticleSystem _knifeAttackEffect;
    [SerializeField] private AudioSource _swingSound;

    private Rigidbody2D _playerBody;
    private float _timeAfterKnifeAttack;

    public bool CanAttack => (_timeAfterKnifeAttack > _secondsBetweenKnifeAttacks);

    private void Awake()
    {
        _knifeAttackEffect = Instantiate(_knifeAttackEffect, transform);
    }

    private void Update()
    {
        _timeAfterKnifeAttack += Time.deltaTime;
    }

    public void Attack()
    {
        if (CanAttack)
        {
            _swingSound.Play();
            _knifeAttackEffect.Play();
            _timeAfterKnifeAttack = 0;
            int direction = (transform.localScale.x > 0) ? 1 : -1;
            RaycastHit2D[] hits = new RaycastHit2D[8];
            int hitCount = _playerBody.Cast(direction * Vector2.right, hits, _knifeAttackRange);

            for (int i = 0; i < hitCount; i++)
                if (hits[i].transform.TryGetComponent(out ITakeDamage target))
                    target.TakeDamage(_damage, transform.position);
        }
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
        int direction = (transform.parent.localScale.x > 0) ? 1 : -1;
        _knifeAttackEffect.transform.position = transform.position + _knifeAttackRange * direction * Vector3.right;
    }

    protected override void Affect(Player player)
    {
        transform.SetParent(player.transform,false);
        transform.position = player.transform.position;
        _playerBody = player.GetComponent<Rigidbody2D>();

        int direction = (player.transform.localScale.x > 0) ? 1 : -1;
        Vector2 effectPosition = transform.position + _knifeAttackRange * direction * Vector3.right;
        _knifeAttackEffect.transform.position = effectPosition;

        player.Inventory.SetDagger(this);
    }

    protected override void StopAffecting(Player player)
    {
        player.Inventory.TakeOffDagger();
    }
}
