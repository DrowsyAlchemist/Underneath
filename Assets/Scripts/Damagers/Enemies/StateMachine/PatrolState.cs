using System.Collections.Generic;
using UnityEngine;

public class PatrolState : State
{
    [Space]
    [Header("Patrol Settings")]
    [SerializeField] private float _walkingSpeed = 1;
    [SerializeField] private float _eachPointDelay = 2;
    [SerializeField] private List<Transform> _patrolPoints;

    private const float DeltaDistance = 0.001f;

    private int _targetPointNumber;
    private Transform _targetPoint;
    private float _elapsedTime;

    private void Start()
    {
        _targetPoint = _patrolPoints[_targetPointNumber];
    }

    private void OnEnable()
    {
        _elapsedTime = 0;
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
        return Vector2.Distance(transform.position, target.position);
    }

    private void MoveToTarget(Transform target)
    {
        transform.TurnToTarget(target);
        EnemyAnimation.PlayWalk();
        float step = _walkingSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
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
    }

    private void Wait()
    {
        EnemyAnimation.PlayIdle();
        _elapsedTime += Time.deltaTime;
    }
}
