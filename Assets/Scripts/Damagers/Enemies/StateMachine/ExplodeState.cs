using System.Collections;
using UnityEngine;


public class ExplodeState : State
{
    [SerializeField] private float _explosionRadius = 2;
    [SerializeField] private ParticleSystem _explosionEffect;
    [SerializeField] private Player _player;

    private void OnEnable()
    {
        StartCoroutine(Explode());
    }

    private IEnumerator Explode()
    {
        Instantiate(_explosionEffect, transform.position, Quaternion.identity);
        EnemyAnimation.PlayAttack();
        yield return new WaitForEndOfFrame();
        yield return new WaitForSeconds(EnemyAnimation.Animator.GetCurrentAnimatorStateInfo(0).length);
        BlowUpPlayer(_player);
        Destroy(gameObject);
    }

    private void BlowUpPlayer(Player player)
    {
        Debug.DrawRay(transform.position, (player.transform.position - transform.position).normalized * _explosionRadius, Color.cyan, 2);

        if ((transform.position - player.transform.position).magnitude < _explosionRadius)
            player.TakeDamage();
    }
}
