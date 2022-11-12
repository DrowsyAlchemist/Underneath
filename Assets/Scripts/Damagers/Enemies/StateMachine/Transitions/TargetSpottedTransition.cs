using UnityEngine;

public class TargetSpottedTransition : EnemyTransition
{
    [SerializeField] private float _viewDirectionInDegrees = 0;
    [SerializeField] private float _viewingAngleInDegrees = 90;
    [SerializeField] private float _viewingRange = 3;

    [SerializeField] private float _targetHeightCorrection = 0.6f;

    private Vector2 _viewDirection;
    private float _viewingAngle;
    private Vector3 _targetPointCorrection;

    private void Start()
    {
        _viewDirection = Quaternion.Euler(0, 0, _viewDirectionInDegrees) * transform.right;
        _viewingAngle = _viewingAngleInDegrees / 2;
        _targetPointCorrection = Vector2.up * _targetHeightCorrection;
    }

    private void FixedUpdate()
    {
        float targetDistance = Vector2.Distance(transform.position, Target.position);
        int positiveDirection = transform.localScale.x > 0 ? 1 : -1;
        Vector2 viewDirection = _viewDirection * positiveDirection;

        Debug.DrawRay(transform.position, Quaternion.Euler(0, 0, _viewingAngle) * viewDirection * _viewingRange, Color.white);
        Debug.DrawRay(transform.position, Quaternion.Euler(0, 0, -1 * _viewingAngle) * viewDirection * _viewingRange, Color.white);

        if (targetDistance < _viewingRange)
        {
            Vector2 targetLocalPosition = Target.position - transform.position + _targetPointCorrection;
            //Vector2 viewDirection = _viewDirection * positiveDirection;
            float playerAngle = Vector2.Angle(viewDirection, targetLocalPosition);

            if (playerAngle < _viewingAngle)
            {
                Debug.DrawRay(transform.position, targetLocalPosition, Color.green);
                NeedTransit = true;
            }
        }
    }
}
