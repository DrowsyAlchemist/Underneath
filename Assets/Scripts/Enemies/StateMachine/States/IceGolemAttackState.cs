using System.Collections;
using UnityEngine;

[RequireComponent(typeof(IceGolemAnimator))]
public class IceGolemAttackState : EnemyState
{
    [SerializeField] private Missile _icicle;
    [SerializeField] private Missile _iceMissile;
    [SerializeField] private float _secondsBetweenLounches;
    [SerializeField] private float _lounchDelay;
    [SerializeField] private float _targetTrashholdHeight;

    private float _elapsedTime;
    private Coroutine _coroutine;
    private Missile _currentMissile;

    private void OnEnable()
    {
        Enemy.Animator.PlayIdle();
        _elapsedTime = _secondsBetweenLounches;
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
            _coroutine = StartCoroutine(ThrowMissile());
    }

    private IEnumerator ThrowMissile()
    {
        _elapsedTime = 0;
        ((IceGolemAnimator)Enemy.Animator).PlayThrow();
        yield return new WaitForEndOfFrame();
        yield return new WaitForSeconds(_lounchDelay);
        LaunchMissile();
    }

    private void LaunchMissile()
    {
        Missile template;
        Vector2 direction = Target.GetPosition() - transform.position;

        if ((Target.GetPosition().y - transform.position.y) < _targetTrashholdHeight)
        {
            template = _iceMissile;
            direction = direction.x * Vector2.right;
        }
        else
        {
            template = _icicle;
        }
        _currentMissile = Instantiate(template, transform.position, Quaternion.identity);
        _currentMissile.Launch(direction);
        _currentMissile = null;
    }

    private void OnValidate()
    {
        if (_secondsBetweenLounches < _lounchDelay)
            _secondsBetweenLounches = _lounchDelay;
    }
}