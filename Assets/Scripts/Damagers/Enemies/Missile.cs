using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
public class Missile : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private int _damage;
    [SerializeField] private LayerMask _collisionLayers;
    [SerializeField] private ParticleSystem _explosionEffect;
    [SerializeField] private float _explosionRadius;

    private ContactFilter2D _filter = new ContactFilter2D();
    private Vector3 _direction;
    private Collider2D _collider;

    private void Awake()
    {
        enabled = false;
        _collider = GetComponent<Collider2D>();
        _collider.enabled = false;
        _filter.useLayerMask = true;
        _filter.layerMask = _collisionLayers;
    }

    public void Launch(Vector2 direction)
    {
        _direction = direction.normalized;
        _collider.enabled = true;
        enabled = true;
    }

    private void Update()
    {
        transform.position += _speed * Time.deltaTime * _direction;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((1 << collision.gameObject.layer & _collisionLayers) > 0)
            Explode();
    }

    private void Explode()
    {
        RaycastHit2D[] hits = new RaycastHit2D[8];
        int hitsCount = Physics2D.CircleCast(transform.position, _explosionRadius, Vector2.zero, _filter, hits, 0);

        for (int i = 0; i < hitsCount; i++)
            if (hits[i].transform.TryGetComponent(out VioletWraith _) == false)
                if (hits[i].transform.TryGetComponent(out ITakeDamage target))
                    target.TakeDamage(_damage, transform.position);

        _explosionEffect.Play();
        _explosionEffect.transform.parent = null;
        Destroy(gameObject);
    }

    private void OnValidate()
    {
        GetComponent<Collider2D>().isTrigger = true;
        GetComponent<Rigidbody2D>().isKinematic = true;
    }
}
