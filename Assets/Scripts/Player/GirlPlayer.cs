using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(AdventureGirlAnimation))]
public class GirlPlayer : Player
{
    [SerializeField] private int _damage;
    [SerializeField] private float _meleeRange;
    [SerializeField] private float _secondsBetweenAttacks;

    private Rigidbody2D _rigidbody;
    private AdventureGirlAnimation _animation;
    private float _counter;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animation = GetComponent<AdventureGirlAnimation>();
    }

    private void Update()
    {
        if (Knocked == false && (_counter > _secondsBetweenAttacks))
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
                KnifeAttack();
            else if (Input.GetKeyDown(KeyCode.Mouse1))
                Shoot();
        }
        else
        {
            _counter += Time.deltaTime;
        }
    }

    private void KnifeAttack()
    {
        _counter = 0;
        _animation.PlayMelee();
        int direction = (transform.localScale.x) > 0 ? 1 : -1;
        RaycastHit2D[] hits = new RaycastHit2D[8];
        int hitCount = _rigidbody.Cast(direction * Vector2.right, hits, _meleeRange);

        for (int i = 0; i < hitCount; i++)
            if (hits[i].transform.TryGetComponent(out ITakeDamage target))
                target.TakeDamage(_damage, gameObject);
    }

    private void Shoot()
    {
        _counter = 0;
        _animation.PlayShoot();
    }
}
