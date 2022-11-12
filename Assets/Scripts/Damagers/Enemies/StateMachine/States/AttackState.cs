using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class AttackState : EnemyState
{
    [SerializeField] private float _attackDelay = 0.3f;
    [SerializeField] private Player _player;

    private Collider2D _collider;

    private void Start()
    {
        _collider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        Attack(_player);
    }

    private void Attack(Player player)
    {
        transform.TurnToTarget(player.transform);
        EnemyAnimation.PlayAttack();
        StartCoroutine(BeatPlayerWithDelay(player));
    }

    private IEnumerator BeatPlayerWithDelay(Player player)
    {
        yield return new WaitForSeconds(_attackDelay);

        if (_collider.IsTouchingLayers(1 << player.gameObject.layer))
            player.TakeDamage();

        enabled = false;
    }

    private void OnValidate()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }
}
