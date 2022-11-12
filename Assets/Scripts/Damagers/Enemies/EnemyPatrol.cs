using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyAnimation))]
public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private float _walkingSpeed = 1;

    [SerializeField] private List<Transform> _patrolPoints;
    [SerializeField] private float _eachPointDelay = 2;

    private const float Delta = 0.001f;

    private EnemyAnimation _enemyAnimation;
    private Coroutine _coroutine;

    private void Start()
    {
        _enemyAnimation = GetComponent<EnemyAnimation>();
        StartPatrol();
    }

    public void StartPatrol()
    {
        _coroutine = StartCoroutine(MoveBetweenPatrolPoints());
    }

    public void StopPatrol()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);
    }

    private IEnumerator MoveBetweenPatrolPoints()
    {
        var waitForSeconds = new WaitForSeconds(_eachPointDelay);

        while (enabled)
        {
            foreach (var point in _patrolPoints)
            {
                if (Vector2.Distance(point.transform.position, transform.position) < Delta)
                    continue;

                transform.TurnToTarget(point);
                _enemyAnimation.PlayWalk();

                while (Vector2.Distance(point.transform.position, transform.position) > Delta)
                {
                    float step = _walkingSpeed * Time.deltaTime;
                    transform.position = Vector3.MoveTowards(transform.position, point.transform.position, step);
                    yield return null;
                }
                _enemyAnimation.PlayIdle();
                yield return waitForSeconds;
            }
            _patrolPoints.Reverse();
        }
    }
}
