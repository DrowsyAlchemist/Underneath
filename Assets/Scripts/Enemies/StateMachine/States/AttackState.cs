using System.Collections;
using UnityEngine;

public class AttackState : EnemyState
{
    [SerializeField] private int _damage = 1;
    [SerializeField] private float _attackRange = 1.5f;
    [SerializeField] private float _attackDelay = 0.3f;
    [SerializeField] private AudioSource _meleeSound;
 
    private void OnEnable()
    {
        transform.TurnToTarget(Target.transform);
        EnemyAnimator.PlayAttack();
        StartCoroutine(BeatPlayerWithDelay(Target));
        enabled = false;
    }

    private IEnumerator BeatPlayerWithDelay(Player player)
    {
        _meleeSound.Play();
        yield return new WaitForSeconds(_attackDelay);

        if (Vector2.Distance(transform.position, player.GetPosition()) < _attackRange)
        {
            if (Enemy.IsAlive)
            player.TakeDamage(_damage, transform.position);
            Debug.DrawRay(transform.position, (player.GetPosition() - transform.position).normalized * _attackRange, Color.red, 1);
        }
    }
}