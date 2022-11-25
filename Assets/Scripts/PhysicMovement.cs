using UnityEngine;

[RequireComponent(typeof(MovementAnimator))]
public class PhysicMovement : MonoBehaviour
{
    [SerializeField] private float _speed;

    [SerializeField] private float _gravityModifier = 4f;
    [SerializeField] private GroundChecker _groundChecker;

    [SerializeField] private Collider2D _collider;
    [SerializeField] private LayerMask _surfases;
    [SerializeField] private float _groundMinNormalY = 0.6f;
    [SerializeField] private float _shellRadius = 0.01f;

    [SerializeField] private float _resistance;

    private const float Delta = 0.1f;
    private Vector2 _gravity = 9.8f * Vector2.down;
    private ContactFilter2D _filter = new ContactFilter2D();
    private Vector2 _gravityVelocity;
    private Vector2 _surfaseVelocity;
    private Vector2 _targetVelocity;
    private bool _canAnimate = true;
    private MovementAnimator _animator;

    public float Speed => _speed;
    public GroundChecker GroundChecker => _groundChecker;

    private void Start()
    {
        _animator = GetComponent<MovementAnimator>();
        _filter.useLayerMask = true;
        _filter.layerMask = _surfases;
    }

    protected void Move()
    {
        SetGravity();

        _targetVelocity = _gravityVelocity + _surfaseVelocity;
        float step = _targetVelocity.magnitude * Time.deltaTime;

        RaycastHit2D[] hits = new RaycastHit2D[1];
        int hitsCount = _collider.Cast(_targetVelocity, _filter, hits, step);

        if (hitsCount > 0)
        {
            Vector2 surfaseNormal = hits[0].normal;
            Vector2 surfaseAlong = new Vector2(surfaseNormal.y, -1 * surfaseNormal.x);

            if (hits[0].normal.y > _groundMinNormalY)                                               // => ground
            {
                _surfaseVelocity = surfaseAlong * _surfaseVelocity.x;
                _gravityVelocity = Vector2.zero;
            }
            else if (hits[0].normal.y < -1 * _groundMinNormalY)                                     // => ceiling
            {
                _gravityVelocity = _gravityModifier * _gravity * Time.deltaTime;
            }
            else                                                                                    // => wall
            {
                _surfaseVelocity.x = 0;
            }
            _targetVelocity = _gravityVelocity + _surfaseVelocity;
            step = _targetVelocity.magnitude * Time.deltaTime;
        }
        hitsCount = _collider.Cast(_targetVelocity, _filter, hits, step + _shellRadius);

        if (hitsCount > 0)
        {
            Debug.Log("Zero");
            step = hits[0].distance - _shellRadius;
        }
        transform.Translate(_targetVelocity.normalized * step);

        if (_canAnimate)
            _animator.AnimateByVelocityAndGrounded(_targetVelocity.normalized * step, _groundChecker.IsGrounded);
    }

    protected void Jump(float jumpForse)
    {
        _gravityVelocity = jumpForse * Vector2.up;
    }

    //private void AdjustVelocityByObstacle()
    //{
    //    RaycastHit2D[] hits = new RaycastHit2D[1];
    //    int hitCount = _collider.Cast(_targetVelocity, _contactFilter, hits, _targetVelocity.magnitude * Time.deltaTime + _shellOffset);

    //    if (hitCount > 0)
    //    {
    //        Vector2 surfaseNormal = hits[0].normal;
    //        Vector2 survaseAlong = new Vector2(surfaseNormal.y, -1 * surfaseNormal.x);
    //        Debug.DrawRay(transform.position, survaseAlong, Color.red, 1);

    //        if (surfaseNormal.y > _groundMinNormalY)                        // => it's a ground
    //            _surfaseVelocity = survaseAlong * _surfaseVelocity.x;
    //        else if (surfaseNormal.y < -1 * _groundMinNormalY)              // => it's a ceiling
    //            AdjustVelocityByCeiling(survaseAlong);
    //        else                                                            // => it's a wall
    //            _surfaseVelocity.x = 0;

    //        _targetVelocity = _gravityVelocity + _surfaseVelocity;
    //    }
    //}

    public void SetVelocity(Vector2 velocity)
    {
        _surfaseVelocity = velocity;
    }

    public void SetVelocityX(float velocityX)
    {
        _surfaseVelocity.x = velocityX;
    }

    public void SetVelocityY(float velocityY)
    {
        _surfaseVelocity.y = velocityY;
    }

    public void AllowAnimation(bool isAllowed)
    {
        _canAnimate = isAllowed;
    }

    private void SetGravity()
    {
        _gravityVelocity += _gravityModifier * _gravity * Time.deltaTime;
    }
}