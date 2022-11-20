using UnityEngine;

[RequireComponent(typeof(EnemyMovement))]
public class ChaseState : EnemyState
{
    [SerializeField] private bool _canFly;
    [SerializeField] private float _speed = 2;

    private EnemyMovement _enemyMovement;

    private void Start()
    {
        _enemyMovement = GetComponent<EnemyMovement>();
    }

    private void OnEnable()
    {
        EnemyAnimation.PlayWalk();
    }

    private void Update()
    {
        transform.TurnToTarget(Target.transform);

        if (_canFly)
            _enemyMovement.MoveToTarget(Target.GetWorldCenter(), _speed);
        else
            _enemyMovement.MoveToTargetAlongXAxis(Target.GetWorldCenter(), _speed);
    }
}
