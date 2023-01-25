using System.Collections;
using UnityEngine;

[RequireComponent(typeof(WraithAnimator))]
[RequireComponent(typeof(EnemyMovement))]
public class ShootState : EnemyState
{
    [SerializeField] private Missile _missile;
    [SerializeField] private Transform _missileLocalPosition;
    [SerializeField] private float _secondsBetweenShots = 1;
    [SerializeField][Range(0, 90)] private float _spreadInDegrees = 40;

    [SerializeField] private float _minTargetDistanse = 4;
    [SerializeField] private float _maxTargetDistanse = 7;
    [SerializeField] private float _lounchDelay = 0.2f;
    [SerializeField] private float _chaseSpeed = 3;
    [SerializeField] private float _fleeSpeed = 2;

    private Missile _currentMissile;
    private float _elapsedTime;
    private WraithAnimator _animator;
    private EnemyMovement _movement;
    private Coroutine _shootCoroutine;

    private void Start()
    {
        _animator = GetComponent<WraithAnimator>();
        _movement = GetComponent<EnemyMovement>();
    }

    private void OnEnable()
    {
        _elapsedTime = _secondsBetweenShots;
    }

    private void OnDisable()
    {
        if (_shootCoroutine != null)
            StopCoroutine(_shootCoroutine);

        if (_currentMissile != null)
            LounchMissile(_currentMissile, dropDown: true);
    }

    private void Update()
    {
        transform.TurnToTarget(Target.transform);
        float distanseToTarget = Vector2.Distance(Target.GetPosition(), transform.position);

        if (distanseToTarget > _maxTargetDistanse || IsObstacleOnWay())
            ChaseTarget();
        else if (distanseToTarget < _minTargetDistanse)
            FleeFromTarget();
        else
            _animator.PlayIdle();

        if (_elapsedTime > _secondsBetweenShots && IsObstacleOnWay() == false)
            _shootCoroutine = StartCoroutine(Shoot());
        else
            _elapsedTime += Time.deltaTime;
    }

    private IEnumerator Shoot()
    {
        _elapsedTime = 0;
        _animator.PlayCastSpell();
        _currentMissile = Instantiate(_missile, _missileLocalPosition.position, Quaternion.identity, transform);
        yield return new WaitForSeconds(_lounchDelay);
        LounchMissile(_currentMissile);
        _currentMissile = null;
    }

    private void LounchMissile(Missile missle, bool dropDown = false)
    {
        Vector2 shotDirection = dropDown ? Vector2.down : CalculateMissileDirection();
        missle.transform.parent = null;
        missle.Launch(shotDirection);
    }

    private Vector2 CalculateMissileDirection()
    {
        float spreadAngle = Random.Range(-1 * _spreadInDegrees / 2, _spreadInDegrees / 2);
        Vector2 shotDirection = Target.GetPosition() - transform.position;
        return Quaternion.Euler(0, 0, spreadAngle) * shotDirection;
    }

    private bool IsObstacleOnWay()
    {
        Vector2 direction = Target.GetPosition() - transform.position;
        RaycastHit2D[] hits = new RaycastHit2D[1];
        ContactFilter2D filter = _movement.Obstacles;
        float distance = direction.magnitude;
        Debug.DrawRay(transform.position, direction);
        return Physics2D.Raycast(transform.position, direction, filter, hits, distance) > 0;
    }

    private void ChaseTarget()
    {
        _animator.PlayWalk();
        _movement.MoveToTarget(Target.GetPosition(), _chaseSpeed);
    }

    private void FleeFromTarget()
    {
        _animator.PlayWalk();
        _movement.MoveFromTarget(Target.GetPosition(), _fleeSpeed);
    }

    private void OnValidate()
    {
        _secondsBetweenShots = Mathf.Abs(_secondsBetweenShots);
        _lounchDelay = Mathf.Abs(_lounchDelay);

        if (_secondsBetweenShots < _lounchDelay)
            _secondsBetweenShots = _lounchDelay;
    }
}