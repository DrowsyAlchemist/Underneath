using System.Collections;
using UnityEngine;

[RequireComponent(typeof(WraithAnimator))]
[RequireComponent(typeof(MovementInFlight))]
public class ShootState : EnemyState
{
    [SerializeField] private Missile _missile;
    [SerializeField] private Transform _missileLocalPosition;
    [SerializeField] private float _secondsBetweenShots = 1;
    [SerializeField][Range(0, 90)] private float _spreadInDegrees;

    [SerializeField] private float _minTargetDistanse;
    [SerializeField] private float _maxTargetDistanse;
    [SerializeField] private float _lounchDelay = 0.2f;
    [SerializeField] private float _chaseSpeed;
    [SerializeField] private float _fleeSpeed;

    private Missile _currentMissile;
    private float _elapsedTime;
    private WraithAnimator _animator;
    private MovementInFlight _movementInFlight;

    private void Start()
    {
        _animator = GetComponent<WraithAnimator>();
        _movementInFlight = GetComponent<MovementInFlight>();
    }

    private void OnEnable()
    {
        _elapsedTime = _secondsBetweenShots;
    }

    private void Update()
    {
        transform.TurnToTarget(Target.transform);
        Vector2 targetLocalPosition = Target.GetWorldCenter() - transform.position;
        float distanseToTarget = targetLocalPosition.magnitude;

        if (distanseToTarget > _maxTargetDistanse)
            ChaseTarget(targetLocalPosition);
        else if (distanseToTarget < _minTargetDistanse)
            FleeFromTarget(targetLocalPosition);
        else
            _animator.PlayIdle();

        if (_elapsedTime > _secondsBetweenShots)
            StartCoroutine(Shoot());
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

    private void LounchMissile(Missile missle)
    {
        float spreadAngle = Random.Range(-1 * _spreadInDegrees / 2, _spreadInDegrees / 2);
        Vector2 shotDirection = Target.GetWorldCenter() - transform.position;
        shotDirection = Quaternion.Euler(0, 0, spreadAngle) * shotDirection;
        missle.transform.parent = null;
        missle.Launch(shotDirection, Target);
    }

    private void ChaseTarget(Vector2 targetLocalPosition)
    {
        _animator.PlayWalk();
        Vector2 direction = targetLocalPosition.normalized;
        float step = _chaseSpeed * Time.deltaTime;
        transform.Translate(step * direction);
        //_movementInFlight.MakeStepToTarget(Target.GetWorldCenter(), _chaseSpeed);
    }

    private void FleeFromTarget(Vector2 targetLocalPosition)
    {
        _animator.PlayWalk();
        Vector2 direction = -1 * targetLocalPosition.normalized;
        transform.Translate(_fleeSpeed * Time.deltaTime * direction);
    }

    private void OnValidate()
    {
        _secondsBetweenShots = Mathf.Abs(_secondsBetweenShots);
        _lounchDelay = Mathf.Abs(_lounchDelay);

        if (_secondsBetweenShots < _lounchDelay)
            _secondsBetweenShots = _lounchDelay;
    }
}
