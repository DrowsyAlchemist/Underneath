using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IceGolemAnimator))]
public class CastIceMissileState : EnemyState
{
    [SerializeField] private Missile _iceMissile;
    [SerializeField] private Transform _lounchPoint;
    [SerializeField] private float _secondsBetweenLounches;
    [SerializeField] private float _lounchDelay;

    private float _elapsedTime;
    private Coroutine _coroutine;
    private Missile _currentMissile;

    private void OnEnable()
    {
        EnemyAnimator.PlayIdle();
        _elapsedTime = _secondsBetweenLounches / 2;
    }

    private void OnDisable()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        if (_currentMissile != null)
            _currentMissile.Launch((transform.localScale.x * Vector2.right).normalized);
    }

    private void Update()
    {
        _elapsedTime += Time.deltaTime;
        transform.TurnToTarget(Target.transform);

        if (_elapsedTime > _secondsBetweenLounches)
        {
            _elapsedTime = 0;
            _coroutine = StartCoroutine(ThrowMissile());
        }
    }

    private IEnumerator ThrowMissile()
    {
        ((IceGolemAnimator)Enemy.EnemyAnimator).PlayThrow();
        yield return new WaitForEndOfFrame();
        yield return new WaitForSeconds(_lounchDelay);
        _currentMissile = Instantiate(_iceMissile, _lounchPoint.position, Quaternion.identity);
        _currentMissile.transform.LookForwardDirection(_lounchPoint.position - transform.position);
        _currentMissile.Launch(transform.localScale.x * Vector2.right);
        _currentMissile = null;
    }

    private void OnValidate()
    {
        if (_secondsBetweenLounches < _lounchDelay)
            _secondsBetweenLounches = _lounchDelay;
    }
}
