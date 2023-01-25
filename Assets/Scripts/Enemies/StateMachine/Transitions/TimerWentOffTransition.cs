using UnityEngine;

[RequireComponent(typeof(EnemyAnimator))]
public class TimerWentOffTransition : EnemyTransition
{
    [SerializeField] private float _seconds = 2;

    private float _elapsedTime;

    private void Update()
    {
        _elapsedTime += Time.deltaTime;

        if (_elapsedTime> _seconds)
        {
            NeedTransit = true;
            _elapsedTime = 0;
        }
    }
}