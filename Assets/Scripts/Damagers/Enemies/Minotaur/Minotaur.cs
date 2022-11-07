using System.Collections;
using UnityEngine;

[RequireComponent(typeof(EnemyAnimation))]
public class Minotaur : Enemy
{
    [SerializeField] private float _attackDelay = 0.3f;

    protected override void Attack(Player player)
    {
        base.Attack(player);
        StartCoroutine(BeatPlayerWithDelay(player));
    }

    private IEnumerator BeatPlayerWithDelay(Player player)
    {
        yield return new WaitForSeconds(_attackDelay);

        if (Collider.IsTouchingLayers(1 << player.gameObject.layer))
            player.TakeDamage();
    }
}
