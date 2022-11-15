using UnityEngine;

public class ChaseState : EnemyState
{
    [SerializeField] private bool _canFly;
    [SerializeField] private float _speed = 2;

    private void OnEnable()
    {
        EnemyAnimation.PlayWalk();
    }

    private void Update()
    {
        MoveToTarget();
    }

    private void MoveToTarget()
    {
        transform.TurnToTarget(Target.transform);
        float step = _speed * Time.deltaTime;
        Vector2 targetDirection = (Target.GetWorldCenter() - transform.position).normalized;

        if (_canFly)
            transform.Translate(targetDirection * step);
        else
            transform.Translate((targetDirection.x > 0 ? 1 : -1) * step * Vector2.right);
    }
}
