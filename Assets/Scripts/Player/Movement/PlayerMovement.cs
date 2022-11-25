using UnityEngine;

[RequireComponent(typeof(MovementAnimator))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForse;
    [SerializeField] public float _gravityModifier = 4f;
    [SerializeField] private GroundChecker _groundChecker;

    [SerializeField] private Collider2D _collider;
    [SerializeField] private LayerMask _surfases;
    [SerializeField] private float _shellRadius = 0.01f;

    [SerializeField] private float _resistance;

    private const float Delta = 0.1f;
    private Vector2 _gravity = 9.8f * Vector2.down;
    private ContactFilter2D _filter = new ContactFilter2D();
    private Vector2 _velocity;
    private Vector2 _additionalVelocity;
    private bool _canInputControlled = true;
    private bool _canAnimate = true;
    private MovementAnimator _animator;

    private void Start()
    {
        _animator = GetComponent<MovementAnimator>();
        _filter.useLayerMask = true;
        _filter.layerMask = _surfases;
    }

    private void Update()
    {
        // _additionalVelocity = Vector2.MoveTowards(_additionalVelocity, Vector2.zero, _resistance * Time.deltaTime);
        if (_canInputControlled)
            GetInputVelocity();
        else
            _velocity.x = 0;

        SetGravity();

        Vector2 resultVelocity = _velocity + _additionalVelocity;
        float step = resultVelocity.magnitude * Time.deltaTime;

        Debug.DrawRay(transform.position, resultVelocity);

        RaycastHit2D[] hits = new RaycastHit2D[1];
        int hitsCount = _collider.Cast(resultVelocity, _filter, hits, step);

        if (hitsCount > 0)
        {
            if (hits[0].normal.y > Delta)                                               // => ground
                _velocity.y = 0;
            else if (hits[0].normal.y < -1 * Delta)                                     // => ceiling
                _velocity.y = _gravityModifier * _gravity.magnitude * Time.deltaTime;
            else                                                                        // => wall
                _velocity.x = 0;

            resultVelocity = _velocity + _additionalVelocity;
            step = resultVelocity.magnitude * Time.deltaTime;
        }
        hitsCount = _collider.Cast(resultVelocity, _filter, hits, step);

        if (hitsCount > 0)
            step = hits[0].distance - _shellRadius;

        transform.Translate(resultVelocity.normalized * step);

        if (_canAnimate)
            _animator.AnimateByVelocity(resultVelocity);
    }

    public void AllowAnimation(bool isAllowed)
    {
        _canAnimate = isAllowed;
    }

    public void AllowInpupControl(bool isAllowed)
    {
        _canInputControlled = isAllowed;
    }

    public void AddForse(Vector2 forse)
    {
        _additionalVelocity = forse;
    }

    private void GetInputVelocity()
    {
        if (Input.GetKey(KeyCode.D))
            _velocity.x = _speed;
        else if (Input.GetKey(KeyCode.A))
            _velocity.x = -1 * _speed;
        else
            _velocity.x = 0;

        if (Input.GetKeyDown(KeyCode.Space))
            Jump();
    }

    private void Jump()
    {
        if (_groundChecker.IsGrounded)
            _velocity.y = _jumpForse;
    }

    private void SetGravity()
    {
        if (_groundChecker.IsGrounded == false)
            _velocity += _gravityModifier * _gravity * Time.deltaTime;
        else if (_velocity.y != _jumpForse)
            _velocity.y = 0;
    }
}