using System.Collections;
using UnityEngine;

public class Wraith : Enemy
{
    [SerializeField] private float _explosionRadius = 2;
    [SerializeField] private ParticleSystem _explosionEffect;

    private bool _isExploded;

    protected override void Attack(Player player)
    {
        if (_isExploded == false)
        {
            _isExploded = true;
            base.Attack(player);
            StartCoroutine(BlowUpPlayer(player));
        }
    }

    private IEnumerator BlowUpPlayer(Player player)
    {
        Instantiate(_explosionEffect, transform.position, Quaternion.identity);
        yield return new WaitForEndOfFrame();
        yield return new WaitForSeconds(EnemyAnimation.Animator.GetCurrentAnimatorStateInfo(0).length);

        Debug.DrawRay(transform.position, (player.transform.position - transform.position).normalized * _explosionRadius, Color.cyan, 2);

        if ((transform.position - player.transform.position).magnitude < _explosionRadius)
            player.TakeDamage();

        gameObject.SetActive(false);
    }
}