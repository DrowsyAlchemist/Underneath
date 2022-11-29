using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(AdventureGirlAnimation))]
public class GirlPlayer : Player
{
    [SerializeField] private int _damage;
    [SerializeField] private float _knifeAttackRange;
    [SerializeField] private float _secondsBetweenKnifeAttacks;
    [SerializeField] private float _secondsBetweenShoots;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private Bullet _bullet;
    [SerializeField] private ParticleSystem _knifeAttackEffect;

    private Rigidbody2D _rigidbody;
    private AdventureGirlAnimation _animation;
    private float _timeAfterKnifeAttack;
    private float _timeAfterShot;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animation = GetComponent<AdventureGirlAnimation>();
        _knifeAttackEffect.transform.position = transform.position + _knifeAttackRange * Vector3.right;
    }

    private void Update()
    {
        if (Knocked == false)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (_timeAfterKnifeAttack > _secondsBetweenKnifeAttacks)
                    KnifeAttack();
            }
            else if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                if (_timeAfterShot > _secondsBetweenShoots)
                    Shoot();
            }
        }
        _timeAfterKnifeAttack += Time.deltaTime;
        _timeAfterShot += Time.deltaTime;
    }

    private void KnifeAttack()
    {
        _knifeAttackEffect.Play();
        _timeAfterKnifeAttack = 0;
        _animation.PlayMelee();
        int direction = (transform.localScale.x > 0) ? 1 : -1;
        RaycastHit2D[] hits = new RaycastHit2D[8];
        int hitCount = _rigidbody.Cast(direction * Vector2.right, hits, _knifeAttackRange);

        for (int i = 0; i < hitCount; i++)
            if (hits[i].transform.TryGetComponent(out ITakeDamage target))
                target.TakeDamage(_damage, transform.position);
    }

    private void Shoot()
    {
        _timeAfterShot = 0;
        _animation.PlayShoot();
        Bullet bullet = Instantiate(_bullet, _shootPoint.position, Quaternion.identity);
        int positiveDirection = (transform.localScale.x > 0) ? 1 : -1;
        bullet.Launch(positiveDirection * Vector2.right);
    }
}