using System.Collections;
using UnityEngine;

public class ShootState : EnemyState
{
    [SerializeField] private Missile _missile;
    [SerializeField] private Transform _missileLocalPosition;
    [SerializeField] private float _secondsBetweenShots = 1;
    [SerializeField][Range(0, 90)] private float _spreadInDegrees;

    private Coroutine _coroutine;

    private void OnEnable()
    {
        _coroutine = StartCoroutine(Shoot());
    }

    private void OnDisable()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);
    }

    private IEnumerator Shoot()
    {
        var waitForSecondsBetweenShots = new WaitForSeconds(_secondsBetweenShots);

        while (enabled)
        {
            float spreadAngle = Random.Range(-1 * _spreadInDegrees / 2, _spreadInDegrees / 2);
            Missile missile = Instantiate(_missile, _missileLocalPosition.position, Quaternion.identity, transform);
            Vector2 shotDirection = Target.GetWorldCenter() - transform.position;
            shotDirection = Quaternion.Euler(0, 0, spreadAngle) * shotDirection;
            missile.Launch(shotDirection);
            yield return waitForSecondsBetweenShots;
        }
    }
}
