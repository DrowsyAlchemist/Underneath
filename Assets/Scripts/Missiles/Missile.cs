using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
public class Missile : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private int _damage;
    [SerializeField] private ParticleSystem _hitEffectTemplate;
    [SerializeField] private LayerMask _collisionLayers;
    [SerializeField] private AudioSource _lounchSound;
    [SerializeField] private AudioSource _hitSound;

    protected ContactFilter2D ContactFilter = new();
    private Vector3 _direction;
    private Collider2D _collider;

    private void Awake()
    {
        enabled = false;
        _collider = GetComponent<Collider2D>();
        _collider.enabled = false;
        ContactFilter.useLayerMask = true;
        ContactFilter.layerMask = _collisionLayers;
    }

    public void IncreaseDamage(int value)
    {
        if (value < 0)
            throw new System.ArgumentOutOfRangeException("value");

        _damage += value;
    }

    public void Launch(Vector2 direction)
    {
        if(_lounchSound)
            _lounchSound.Play();

        _direction = direction.normalized;
        RotateMissile(direction);
        _collider.enabled = true;
        enabled = true;
    }

    protected virtual void RotateMissile(Vector2 initialDirection)
    {
        return;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.position += _speed * Time.deltaTime * _direction;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((1 << collision.gameObject.layer & _collisionLayers) > 0)
            Collapse(collision);
    }

    private void Collapse(Collider2D collision)
    {
        DoDamage(collision, _damage);
        Instantiate(_hitEffectTemplate, transform.position, Quaternion.identity, null);
        _hitSound.Play();
        _hitSound.transform.SetParent(null);
        Destroy(gameObject);
    }

    protected virtual void DoDamage(Collider2D collision, int damage)
    {
        if (collision.transform.TryGetComponent(out ITakeDamage subject))
            subject.TakeDamage(damage, transform.position);
    }

    private void OnValidate()
    {
        GetComponent<Collider2D>().isTrigger = true;
        GetComponent<Rigidbody2D>().isKinematic = true;
    }
}