using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Missile : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private int _damage;
    [SerializeField] private LayerMask _collisionLayers;
    [SerializeField] private ParticleSystem _missileEffect;
    [SerializeField] private ParticleSystem _explosionEffect;
    [SerializeField] private float _explosionRadius;

    private Vector3 _direction;
    private Player _target;

    private void Awake()
    {
        enabled = false;
    }

    private void Update()
    {
        transform.position += _speed * Time.deltaTime * _direction;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject)
            if ((1 << collision.gameObject.layer & _collisionLayers) > 0)
                Explode();
    }

    public void Launch(Vector2 direction, Player target)
    {
        _target = target;
        _direction = direction.normalized;
        enabled = true;
    }

    private void Explode()
    {
        if (Vector2.Distance(_target.GetWorldCenter(), transform.position) < _explosionRadius)
            _target.TakeDamage(_damage);

        _missileEffect.Stop();
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
