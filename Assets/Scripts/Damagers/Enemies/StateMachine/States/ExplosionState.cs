using System.Collections;
using UnityEngine;

public class ExplosionState : EnemyState
{
    [SerializeField] private int _damage = 1;
    [SerializeField] private float _explosionRadius = 2;
    [SerializeField] private ParticleSystem _explosionEffect;

    [SerializeField] private float _speedDuringExplosion;

    private void OnEnable()
    {
        StartCoroutine(Explode());
    }

    private void Update()
    {
        float step = _speedDuringExplosion * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, Target.GetWorldCenter(), step);
    }

    private IEnumerator Explode()
    {
        ParticleSystem effect = Instantiate(_explosionEffect, transform.position, Quaternion.identity, transform);
        EnemyAnimation.PlayAttack();
        yield return new WaitForEndOfFrame();
        yield return new WaitForSeconds(EnemyAnimation.Animator.GetCurrentAnimatorStateInfo(0).length);

        if (Target.TryGetComponent(out Player player))
            BlowUpPlayer(player);

        effect.transform.parent = null;
        Destroy(gameObject);
    }

    private void BlowUpPlayer(Player player)
    {
        Debug.DrawRay(transform.position, (player.GetWorldCenter() - transform.position).normalized * _explosionRadius, Color.cyan, 2);

        if ((transform.position - player.GetWorldCenter()).magnitude < _explosionRadius)
            player.TakeDamage(_damage);
    }
}
