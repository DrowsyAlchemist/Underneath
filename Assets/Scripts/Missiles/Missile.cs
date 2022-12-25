using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
public class Missile : MonoBehaviour
{
    [SerializeField] protected float Speed;
    [SerializeField] protected int Damage;
    [SerializeField] protected ParticleSystem HitEffectTemplate;
    [SerializeField] private LayerMask _collisionLayers;

    protected ContactFilter2D Filter = new ContactFilter2D();
    private Vector3 _direction;
    private Collider2D _collider;

    private void Awake()
    {
        enabled = false;
        _collider = GetComponent<Collider2D>();
        _collider.enabled = false;
        Filter.useLayerMask = true;
        Filter.layerMask = _collisionLayers;
    }

    public virtual void Launch(Vector2 direction)
    {
        _direction = direction.normalized;
        _collider.enabled = true;
        enabled = true;
    }

    public void IncreaseDamage(int value)
    {
        if (value < 0)
            throw new System.ArgumentOutOfRangeException("value");

        Damage += value;
    }

    private void Update()
    {
        Move();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((1 << collision.gameObject.layer & _collisionLayers) > 0)
            Hit(collision);
    }

    protected virtual void Hit(Collider2D collision)
    {
        if (collision.transform.TryGetComponent(out ITakeDamage target))
            target.TakeDamage(Damage, transform.position);

        Collapse();
    }
    protected void Collapse()
    {
        Instantiate(HitEffectTemplate, transform.position, Quaternion.identity, null);
        Destroy(gameObject);
    }

    private void Move()
    {
        transform.position += Speed * Time.deltaTime * _direction;
    }

    private void OnValidate()
    {
        GetComponent<Collider2D>().isTrigger = true;
        GetComponent<Rigidbody2D>().isKinematic = true;
    }
}
