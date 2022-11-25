using UnityEngine;

[RequireComponent(typeof(MovementAnimator))]
public class PhysicMovement : MonoBehaviour
{
    [SerializeField] protected float Speed;
    [SerializeField] private float _jumpForse;
    [SerializeField] public float _gravityModifier = 4f;

    [SerializeField] private GroundChecker _groundChecker;

    [SerializeField] private Collider2D _collider;
    [SerializeField] private LayerMask _surfases;
    [SerializeField] private float _shellRadius = 0.01f;

    [SerializeField] private float _resistance;

    protected Vector2 Velocity;
    private const float Delta = 0.1f;
    private Vector2 _gravity = 9.8f * Vector2.down;
    private ContactFilter2D _filter = new ContactFilter2D();
    private Vector2 _additionalVelocity;
    private bool _canAnimate = true;
    private MovementAnimator _animator;

    private void Start()
    {
        _animator = GetComponent<MovementAnimator>();
        _filter.useLayerMask = true;
        _filter.layerMask = _surfases;
    }

    protected virtual void Update()
    {
        _additionalVelocity = Vector2.MoveTowards(_additionalVelocity, Vector2.zero, _resistance * Time.deltaTime);

        SetGravity();

        Vector2 resultVelocity = Velocity + _additionalVelocity;
        float step = resultVelocity.magnitude * Time.deltaTime;

        Debug.DrawRay(transform.position, resultVelocity);

        RaycastHit2D[] hits = new RaycastHit2D[1];
        int hitsCount = _collider.Cast(resultVelocity, _filter, hits, step);

        if (hitsCount > 0)
        {
            if (hits[0].normal.y > Delta)                                               // => ground
                Velocity.y = 0;
            else if (hits[0].normal.y < -1 * Delta)                                     // => ceiling
                Velocity.y = _gravityModifier * _gravity.magnitude * Time.deltaTime;
            else                                                                        // => wall
                Velocity.x = 0;

            resultVelocity = Velocity + _additionalVelocity;
            step = resultVelocity.magnitude * Time.deltaTime;
        }
        hitsCount = _collider.Cast(resultVelocity, _filter, hits, step);

        if (hitsCount > 0)
            step = hits[0].distance - _shellRadius;

        transform.Translate(resultVelocity.normalized * step);

        if (_canAnimate)
            _animator.AnimateByVelocity(resultVelocity);
    }

    public void SetVelocity(Vector2 velocity)
    {
        Velocity = velocity;
    }

    public void AddForse(Vector2 forse)
    {
        _additionalVelocity = forse;
    }

    public void Jump()
    {
        if (_groundChecker.IsGrounded)
            Velocity.y = _jumpForse;
    }

    public void AllowAnimation(bool isAllowed)
    {
        _canAnimate = isAllowed;
    }

    private void SetGravity()
    {
        if (_groundChecker.IsGrounded && Velocity.y < (_jumpForse - Delta))
            Velocity.y = 0;
        else
            Velocity += _gravityModifier * _gravity * Time.deltaTime;
    }
}
