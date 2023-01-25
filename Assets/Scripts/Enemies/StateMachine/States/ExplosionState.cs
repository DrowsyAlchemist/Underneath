using System.Collections;
using UnityEngine;

public class ExplosionState : EnemyState
{
    [SerializeField] private int _damage = 1;
    [SerializeField] private float _explosionRadius = 2;
    [SerializeField] private ParticleSystem _explosionEffect;
    [SerializeField] private AudioSource _explosionSound;

    [SerializeField] private float _speedDuringExplosion;
    [SerializeField] private float _minDistanse;

    private Coroutine _coroutine;

    private void OnEnable()
    {
        if (_coroutine == null)
            _coroutine = StartCoroutine(Explode());
    }

    private void Update()
    {
        if (Vector2.Distance(Target.GetPosition(), transform.position) > _minDistanse)
            Enemy.Movement.MoveToTarget(Target.GetPosition(), _speedDuringExplosion);
    }

    private IEnumerator Explode()
    {
        ParticleSystem effect = Instantiate(_explosionEffect, transform.position, Quaternion.identity, transform);
        EnemyAnimator.PlayAttack();
        yield return new WaitForEndOfFrame();
        yield return new WaitForSeconds(EnemyAnimator.Animator.GetCurrentAnimatorStateInfo(0).length);
        BlowUpPlayer(Target);
        effect.transform.parent = null;
        _explosionSound.Play();
        _explosionSound.transform.SetParent(null);

        if (gameObject)
            Destroy(gameObject);
    }

    private void BlowUpPlayer(Player player)
    {
        Debug.DrawRay(transform.position, (player.GetPosition() - transform.position).normalized * _explosionRadius, Color.cyan, 2);

        if ((transform.position - player.GetPosition()).magnitude < _explosionRadius)
            player.TakeDamage(_damage, transform.position);
    }
}