using UnityEngine;

[RequireComponent(typeof(EnemyAnimator))]
public class FallingState : EnemyState
{
    [SerializeField] private float _fallingAcceleration = 9.8f;

    private EnemyAnimator _enemyAnimator;
    private float _fallingSpeed;

    private void OnEnable()
    {
        if(_enemyAnimator)
            _enemyAnimator.PlayIdle();
        else
            _enemyAnimator = GetComponent<EnemyAnimator>();

        _fallingSpeed = 0;
    }

    private void Update()
    {
        _fallingSpeed += _fallingAcceleration * Time.deltaTime;
        transform.Translate(_fallingSpeed * Time.deltaTime * Vector2.down);
    }
}