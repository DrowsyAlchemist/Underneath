using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(GroundCheck))]
[RequireComponent(typeof(MovementAnimation))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] public float _gravityModifier = 1.5f;
    [SerializeField] private float _speed = 3;
    [Space]
    [Header("Surface settings")]
    [SerializeField] private LayerMask _surfaceLayers;
    [SerializeField] private float _groundMinNormalY = 0.6f;
    [SerializeField] private float _shellOffset = 0.03f;
    [SerializeField] private float _groundDistanceModifier = 1.1f;
    [Space]
    [Header("Jump settings")]
    [SerializeField] private float _jumpForse = 3;
    [SerializeField] private int _maxJumpCount = 2;
    [SerializeField] private float _timeBeforeLanding = 0.08f;
    [SerializeField] private float _timeAflerGetOffGround = 0.15f;

    private readonly Vector2 _gravity = 9.8f * Vector2.down;
    private Vector2 _gravityVelocity;
    private Vector2 _surfaseVelocity;
    private Vector2 _targetVelocity;

    private Collider2D _collider;
    private GroundCheck _groundChecker;
    private bool _isGrounded;

    private int _jumpsLeft;
    private bool _spaceIsPressed;
    private float _secondsInAir;
    private float _secondsAfterSpacePressed;

    private MovementAnimation _movementAnimator;
    private bool _canAnimate = true;
    private bool _canInputControlled = true;

    private ContactFilter2D _contactFilter = new ContactFilter2D();

    private void OnEnable()
    {
        GetComponent<Rigidbody2D>().isKinematic = true;
    }

    private void Start()
    {
        _groundChecker = GetComponent<GroundCheck>();
        _collider = GetComponent<Collider2D>();
        _movementAnimator = GetComponent<MovementAnimation>();
        _contactFilter.useLayerMask = true;
        _contactFilter.layerMask = _surfaceLayers;
    }

    public void AllowAnimation(bool isAllowed)
    {
        _canAnimate = isAllowed;
    }

    public void AllowInpupControl(bool isAllowed)
    {
        _canInputControlled = isAllowed;
    }

    private void Update()
    {
        _isGrounded = _groundChecker.IsGrounded(_shellOffset * _groundDistanceModifier);

        _surfaseVelocity = _canInputControlled ? GetVelocityFromKeyboard() : Vector2.zero;
        _gravityVelocity = CalculateGravity(_gravityVelocity);

        if (_canInputControlled)
            SmartJump();

        _targetVelocity = _gravityVelocity + _surfaseVelocity;
        Debug.DrawRay(transform.position, _targetVelocity, Color.white);

        AdjustVelocityByObstacle();
        Debug.DrawRay(transform.position, _targetVelocity, Color.blue);

        Vector2 move = CalculateMove();
        transform.Translate(move);

        if (_movementAnimator && _canAnimate)
            _movementAnimator.AnimateByVelocityAndGrounded(move, _isGrounded);
    }

    private Vector2 GetVelocityFromKeyboard()
    {
        if (Input.GetKey(KeyCode.D))
            return _speed * Vector2.right;
        else if (Input.GetKey(KeyCode.A))
            return -1 * _speed * Vector2.right;
        else
            return Vector2.zero;
    }

    private Vector2 CalculateGravity(Vector2 currentGravity)
    {
        if (_isGrounded == false)
            return currentGravity + _gravityModifier * Time.deltaTime * _gravity;
        else if (currentGravity.y < 0)
            return Vector2.zero;
        else
            return currentGravity;
    }

    private void AdjustVelocityByObstacle()
    {
        RaycastHit2D[] hits = new RaycastHit2D[1];
        int hitCount = _collider.Cast(_targetVelocity, _contactFilter, hits, _targetVelocity.magnitude * Time.deltaTime + _shellOffset);

        if (hitCount > 0)
        {
            Vector2 surfaseNormal = hits[0].normal;
            Vector2 survaseAlong = new Vector2(surfaseNormal.y, -1 * surfaseNormal.x);
            Debug.DrawRay(transform.position, survaseAlong, Color.red, 1);

            if (surfaseNormal.y > _groundMinNormalY)                        // => it's a ground
                _surfaseVelocity = survaseAlong * _surfaseVelocity.x;
            else if (surfaseNormal.y < -1 * _groundMinNormalY)              // => it's a ceiling
                AdjustVelocityByCeiling(survaseAlong);
            else                                                            // => it's a wall
                _surfaseVelocity.x = 0;

            _targetVelocity = _gravityVelocity + _surfaseVelocity;
        }
    }

    private void AdjustVelocityByCeiling(Vector2 ceilingAlong)
    {
        _surfaseVelocity = -1 * ceilingAlong * _surfaseVelocity.x;

        if (_gravityVelocity.y > 0)
            _gravityVelocity.y = 0;
    }

    private Vector2 CalculateMove()
    {
        RaycastHit2D[] hits = new RaycastHit2D[1];
        Vector2 move = _targetVelocity * Time.deltaTime;
        int hitCount = _collider.Cast(move, _contactFilter, hits, move.magnitude);

        if (hitCount > 0)
            move = move.normalized * (hits[0].distance - _shellOffset);

        return move;
    }

    private void SmartJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_isGrounded || _secondsInAir < _timeAflerGetOffGround)
                JumpFromGround();
            else if (_jumpsLeft > 0)
                JumpInAir();
            else
                _spaceIsPressed = true;

            _secondsAfterSpacePressed = 0;
        }
        else if (_isGrounded)
        {
            if (_spaceIsPressed && _secondsAfterSpacePressed < _timeBeforeLanding)
                JumpFromGround();
            else if (_secondsInAir > 0)
                _jumpsLeft = 0;

            _spaceIsPressed = false;
            _secondsInAir = 0;
        }
        else
        {
            _secondsInAir += Time.deltaTime;
            _secondsAfterSpacePressed += Time.deltaTime;
        }
    }

    private void JumpFromGround()
    {
        _gravityVelocity = _jumpForse * Vector2.up;
        _jumpsLeft = _maxJumpCount - 1;
    }

    private void JumpInAir()
    {
        _gravityVelocity = _jumpForse * Vector2.up;
        _jumpsLeft--;
    }

    private void OnValidate()
    {
        _speed = Mathf.Abs(_speed);
        _jumpForse = Mathf.Abs(_jumpForse);
        _maxJumpCount = Mathf.Abs(_maxJumpCount);
        _timeBeforeLanding = Mathf.Abs(_timeBeforeLanding);
        _timeAflerGetOffGround = Mathf.Abs(_timeAflerGetOffGround);

        _groundMinNormalY = Mathf.Clamp(_groundMinNormalY, -1, 1);

        if (_groundDistanceModifier < 1)
            _groundDistanceModifier = 1;
    }
}