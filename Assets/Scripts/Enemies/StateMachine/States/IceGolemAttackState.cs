using System.Collections;
using UnityEngine;

public class IceGolemAttackState : EnemyState
{
    [SerializeField] private Missile _icicle;
    [SerializeField] private Missile _iceMissile;
    [SerializeField] private float _secondsBetweenLounches;
    [SerializeField] private float _lounchDelay;
    // [SerializeField] private Transform _iceMissileLounchPoint;
    [SerializeField] private float _targetTrashholdHeight;

    private float _elapsedTime;
    private Coroutine _coroutine;
    private Missile _currentMissile;

    private void OnEnable()
    {
        Enemy.EnemyAnimator.PlayIdle();
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
        transform.TurnToTarget(Target.transform);
        _elapsedTime += Time.deltaTime;

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
        Missile template = ChooseMissile();
        LaunchMissile(template);
    }

    private Missile ChooseMissile()
    {
        if ((Target.GetWorldCenter().y - transform.position.y) < _targetTrashholdHeight)
            return _iceMissile;
        else
            return _icicle;
    }

    private void LaunchMissile(Missile template)
    {
        _currentMissile = Instantiate(template, transform.position, Quaternion.identity);
        Vector2 direction = Target.GetWorldCenter() - transform.position;
        _currentMissile.Launch(direction);
        _currentMissile = null;
    }

    private void OnValidate()
    {
        if (_secondsBetweenLounches < _lounchDelay)
            _secondsBetweenLounches = _lounchDelay;
    }
}
