using System.Collections;
using UnityEngine;

[RequireComponent(typeof(EnemyMovement))]
public class AttackState : EnemyState
{
    [SerializeField] private int _damage = 1;
    [SerializeField] private float _attackRange = 1.5f;
    [SerializeField] private float _attackDelay = 0.3f;
 
    private void OnEnable()
    {
        transform.TurnToTarget(Target.transform);
        EnemyAnimator.PlayAttack();
        StartCoroutine(BeatPlayerWithDelay(Target));
        enabled = false;
    }

    private IEnumerator BeatPlayerWithDelay(Player player)
    {
        yield return new WaitForSeconds(_attackDelay);

        if (Vector2.Distance(transform.position, player.GetWorldCenter()) < _attackRange)
        {
            player.TakeDamage(_damage);
            Debug.DrawRay(transform.position, (player.GetWorldCenter() - transform.position).normalized * _attackRange, Color.red, 1);
        }
    }
}
