using UnityEngine;

[RequireComponent(typeof(EnemyAnimator))]
public class EnemyAttackedTransition : EnemyTransition
{
    [SerializeField] private float _delay = 2;

    private float _elapsedTime;

    private void Update()
    {
        _elapsedTime += Time.deltaTime;

        if (_elapsedTime> _delay)
        {
            NeedTransit = true;
            _elapsedTime = 0;
        }
    }
}
