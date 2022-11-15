using System.Collections.Generic;
using UnityEngine;

public class PatrolState : EnemyState
{
    [Space]
    [Header("Patrol Settings")]
    [SerializeField] private bool _canFly;
    [SerializeField] private float _speed = 1;
    [SerializeField] private float _eachPointDelay = 2;
    [SerializeField] private List<Transform> _patrolPoints;

    private const float DeltaDistance = 0.001f;

    private int _targetPointNumber;
    private Transform _targetPoint;
    private float _elapsedTime;

    private void OnEnable()
    {
        _elapsedTime = 0;
        _targetPoint = _patrolPoints[_targetPointNumber];
        transform.TurnToTarget(_targetPoint);
    }

    private void Update()
    {
        if (GetDistanceToTarget(_targetPoint) > DeltaDistance)
            MoveToTarget(_targetPoint);
        else if (_elapsedTime < _eachPointDelay)
            Wait();
        else
            SetNextTargetPoint();
    }

    private float GetDistanceToTarget(Transform target)
    {
        if (_canFly)
            return Vector2.Distance(transform.position, target.position);
        else
            return Mathf.Abs(transform.position.x - target.position.x);
    }

    private void MoveToTarget(Transform target)
    {
        EnemyAnimation.PlayWalk();
        float step = _speed * Time.deltaTime;
        Vector2 targetDirection = (target.position - transform.position).normalized;

        if (_canFly)
            transform.Translate(targetDirection * step);
        else
            transform.Translate((targetDirection.x > 0 ? 1 : -1) * step * Vector2.right);
    }

    private void SetNextTargetPoint()
    {
        if (_targetPointNumber + 1 < _patrolPoints.Count)
        {
            _targetPointNumber++;
        }
        else
        {
            _patrolPoints.Reverse();
            _targetPointNumber = 1;
        }
        _targetPoint = _patrolPoints[_targetPointNumber];
        _elapsedTime = 0;
        transform.TurnToTarget(_targetPoint);
    }

    private void Wait()
    {
        EnemyAnimation.PlayIdle();
        _elapsedTime += Time.deltaTime;
    }
}
