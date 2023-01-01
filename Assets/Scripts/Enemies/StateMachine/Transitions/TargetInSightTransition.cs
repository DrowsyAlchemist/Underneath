using UnityEngine;

public class TargetInSightTransition : EnemyTransition
{
    [SerializeField] private float _viewDirectionInDegrees = 0;
    [SerializeField] private float _viewingAngleInDegrees = 90;
    [SerializeField] private float _viewingRange = 3;

    private Vector2 _viewDirection;
    private float _viewingAngle;
 
    private void Start()
    {
        _viewDirection = Quaternion.Euler(0, 0, _viewDirectionInDegrees) * transform.right;
        _viewingAngle = _viewingAngleInDegrees / 2;
    }

    private void FixedUpdate()
    {
        float targetDistance = Vector2.Distance(transform.position, Target.GetWorldCenter());
        int positiveDirection = transform.localScale.x > 0 ? 1 : -1;
        Vector2 viewDirection = _viewDirection * positiveDirection;

        Debug.DrawRay(transform.position, Quaternion.Euler(0, 0, _viewingAngle) * viewDirection * _viewingRange, Color.white);
        Debug.DrawRay(transform.position, Quaternion.Euler(0, 0, -1 * _viewingAngle) * viewDirection * _viewingRange, Color.white);

        if (targetDistance < _viewingRange)
        {
            Vector2 targetLocalPosition = Target.GetWorldCenter() - transform.position;
            float playerAngle = Vector2.Angle(viewDirection, targetLocalPosition);

            if (playerAngle < _viewingAngle)
            {
                Debug.DrawRay(transform.position, targetLocalPosition, Color.green);
                NeedTransit = true;
            }
        }
    }
}
