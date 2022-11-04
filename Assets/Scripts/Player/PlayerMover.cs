using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(GroundChecker))]
[RequireComponent(typeof(MovementAnimator))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _speed = 3;
    [SerializeField] private float _gravityModifier = 2;
    [SerializeField] private float _groundMinNormalY = 0.6f;

    [SerializeField] private float _jumpForse = 7;
    [SerializeField] private int _maxJumpCount = 1;

    private readonly Vector2 _gravity = 9.8f * Vector2.down;
    private bool _isGrounded;
    private int _jumpCount;

    private Vector2 _targetVelocity;

    private Rigidbody2D _rigidbody;
    private GroundChecker _groundChecker;
    private MovementAnimator _animator;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _groundChecker = GetComponent<GroundChecker>();
        _animator = GetComponent<MovementAnimator>();
    }

    private void Update()
    {
        HandleGravity();
        Jump();
        Move();
        _animator.AnimateByVelocityAndGrounded(_targetVelocity, _isGrounded);
    }

    private void HandleGravity()
    {
        _isGrounded = _groundChecker.IsGrounded();

        if (_isGrounded)
            _targetVelocity.y = 0;
        else
            _targetVelocity += _gravityModifier * Time.deltaTime * _gravity;
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_isGrounded)
            {
                _targetVelocity.y = _jumpForse;
                _jumpCount = 1;
            }
            else if (_jumpCount > 0 && _jumpCount < _maxJumpCount)
            {
                _targetVelocity.y = _jumpForse;
                _jumpCount++;
            }
        }
    }

    private void Move()
    {
        _targetVelocity = ComputeVelocity(_targetVelocity);
        Vector2 move = _targetVelocity * Time.deltaTime;
        transform.Translate(move);
    }

    private Vector2 ComputeVelocity(Vector2 initialVelocity)
    {
        Vector2 resultVelocity = initialVelocity;
        resultVelocity.x = 0;

        if (Input.GetKey(KeyCode.D))
            resultVelocity.x = _speed;
        else if (Input.GetKey(KeyCode.A))
            resultVelocity.x = -1 * _speed;

        resultVelocity = ComputeVelocityAlongSurfase(resultVelocity);


        return resultVelocity;
    }

    private Vector2 ComputeVelocityAlongSurfase(Vector2 initialVelocity)
    {
        Vector2 resultVelocity = initialVelocity;

        RaycastHit2D[] hits = new RaycastHit2D[1];


        int hitCount = _rigidbody.Cast(resultVelocity, hits, resultVelocity.magnitude * Time.deltaTime);


        Debug.DrawRay(transform.position, resultVelocity);
        Debug.Log(resultVelocity);

        if (hitCount > 0)
        {
            //RaycastHit2D closeHit = hits[0];
            //foreach (RaycastHit2D hit in hits)
                //if (hit.distance < closeHit.distance)
                    //closeHit = hit;


            Vector2 surfaseNormal = hits[0].normal;
            Vector2 surfaseAlong = new Vector2(surfaseNormal.y, -1 * surfaseNormal.x);

            Debug.Log(surfaseNormal);
            Debug.DrawRay(transform.position, surfaseNormal, Color.red, 0.5f);

            if (surfaseNormal.y > _groundMinNormalY)
                resultVelocity = resultVelocity.x * surfaseAlong;
            else if (surfaseNormal.y < -1 * _groundMinNormalY)
                resultVelocity.y = 0;
            else
                resultVelocity.x = 0;
        }
        Debug.DrawRay(transform.position, resultVelocity, Color.green, 0.5f);





        hitCount = _rigidbody.Cast(resultVelocity, hits, resultVelocity.magnitude * Time.deltaTime);
        if (hitCount > 0)
        {
            Vector2 surfaseNormal = hits[0].normal;
            Vector2 surfaseAlong = new Vector2(surfaseNormal.y, -1 * surfaseNormal.x);

            Debug.Log(surfaseNormal);
            Debug.DrawRay(transform.position, surfaseNormal, Color.red, 0.5f);

            if (surfaseNormal.y > _groundMinNormalY)
                resultVelocity = resultVelocity.x * surfaseAlong;
            else if (surfaseNormal.y < -1 * _groundMinNormalY)
                resultVelocity.y = 0;
            else
                resultVelocity.x = 0;
        }





        return resultVelocity;
    }

    private void OnValidate()
    {
        GetComponent<Rigidbody2D>().isKinematic = true;
    }
}