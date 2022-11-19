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
    [SerializeField] private float _lounchDelay = 0.2f;

    private Vector3 _direction;

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
        if (collision.TryGetComponent(out Player player))
            player.TakeDamage(_damage);

        if ((1 << collision.gameObject.layer & _collisionLayers) > 0)
            Explode();
    }

    public void Launch(Vector2 direction)
    {
        StartCoroutine(LaunchWithDelay(direction));
    }

    private IEnumerator LaunchWithDelay(Vector2 direction)
    {
        yield return new WaitForSeconds(_lounchDelay);
        _direction = direction.normalized;
        enabled = true;
    }

    private void Explode()
    {
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
