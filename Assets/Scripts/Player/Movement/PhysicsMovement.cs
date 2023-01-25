using UnityEngine;

[RequireComponent(typeof(MovementAnimator))]
public class PhysicsMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _gravityModifier = 4f;
    [SerializeField] private GroundChecker _groundChecker;

    [SerializeField] private Collider2D _collider;
    [SerializeField] private LayerMask _surfases;
    [SerializeField] private float _groundMinNormalY = 0.6f;
    [SerializeField] private float _shellRadius = 0.01f;

    [SerializeField] private float _externalForcesResistance;

    private const float _gravity = 9.8f;
    private ContactFilter2D _filter = new();
    private Vector2 _externalForcesVelocity;
    private Vector2 _surfaceVelocity;
    private Vector2 _targetVelocity;
    private bool _canAnimate = true;
    private MovementAnimator _animator;

    protected float Speed => _speed;
    protected GroundChecker GroundChecker => _groundChecker;

    private void Start()
    {
        _animator = GetComponent<MovementAnimator>();
        _filter.useLayerMask = true;
        _filter.layerMask = _surfases;
    }

    public void AllowAnimation(bool isAllowed)
    {
        _canAnimate = isAllowed;
    }

    public void AddForce(Vector2 force)
    {
        _externalForcesVelocity += force;
    }

    protected void Move()
    {
        _externalForcesVelocity.x = Mathf.MoveTowards(_externalForcesVelocity.x, 0, _externalForcesResistance * Time.deltaTime);
        SetGravity();

        if (TryGetObstacle(out RaycastHit2D obstacle))
            AdjustVelocityByObstacle(obstacle);

        Vector2 step = CalculateStep();
        transform.Translate(step);

        if (_canAnimate)
            _animator.AnimateByVelocityAndGrounded(step, _groundChecker.IsGrounded);
    }

    protected void Jump(float jumpForse)
    {
        _externalForcesVelocity = jumpForse * Vector2.up;
    }

    protected void SetVelocityX(float velocityX)
    {
        _surfaceVelocity.x = velocityX;
    }

    private void SetGravity()
    {
        _externalForcesVelocity += _gravityModifier * _gravity * Time.deltaTime * Vector2.down;
        _targetVelocity = _externalForcesVelocity + _surfaceVelocity;
    }

    private void AdjustVelocityByObstacle(RaycastHit2D obstacle)
    {
        if (obstacle.normal.y > _groundMinNormalY)                                   // => ground
        {
            _surfaceVelocity = GetSurfaceAlong(obstacle) * _surfaceVelocity.x;
            _externalForcesVelocity.y = 0;
        }
        else if (obstacle.normal.y < -1 * _groundMinNormalY)                         // => ceiling
        {
            _externalForcesVelocity.y = _gravityModifier * _gravity * Time.deltaTime;
        }
        else                                                                         // => wall
        {
            _externalForcesVelocity.x = 0;
            _surfaceVelocity.x = 0;
        }
        _targetVelocity = _externalForcesVelocity + _surfaceVelocity;
    }

    private Vector2 CalculateStep()
    {
        if (TryGetObstacle(out RaycastHit2D obstacle))
            return CalculateStepByObstacle(obstacle);
        else
            return (_targetVelocity.magnitude * Time.deltaTime) * _targetVelocity.normalized;
    }

    private Vector2 CalculateStepByObstacle(RaycastHit2D obstacle)
    {
        if (obstacle.distance > _shellRadius)
            return (obstacle.distance - _shellRadius) * _targetVelocity.normalized;
        else
            return obstacle.normal * Time.deltaTime;
    }

    private bool TryGetObstacle(out RaycastHit2D obstacle)
    {
        float step = _targetVelocity.magnitude * Time.deltaTime;
        RaycastHit2D[] hits = new RaycastHit2D[1];
        int hitsCount = _collider.Cast(_targetVelocity, _filter, hits, step + _shellRadius);
        obstacle = hits[0];
        return hitsCount > 0;
    }

    private Vector2 GetSurfaceAlong(RaycastHit2D surface)
    {
        return new Vector2(surface.normal.y, -1 * surface.normal.x);
    }
}