using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float _speed = 3;
    [SerializeField] private float _gravityModifier = 2;
    [SerializeField] private float _castLength = 0;
    [SerializeField] private float _groundMinNormalY = 0.6f;
    [SerializeField] private GroundChecker _groundChecker;
    [SerializeField] private float _jumpForse = 3;
    [SerializeField] private int _maxJumpCount = 2;
    [SerializeField] private ContactFilter2D _filter;
    [SerializeField] private GroundCheck _groundCheck;

    private Collider2D _collider;

    private Vector2 _gravity = 9.8f * Vector2.down;
    private int _jumpCount;

    private Vector2 _gravityVelocity;
    private Vector2 _surfaseVelocity;

    private Vector2 _targetVelocity;


    private bool _isGrounded;

    private void Start()
    {
        _collider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        _surfaseVelocity = GetVelocityFromKeyboard();


        _isGrounded = _groundChecker.IsGrounded();

        if (_isGrounded == false)
            _gravityVelocity += _gravity * _gravityModifier * Time.deltaTime;
        else
            _gravityVelocity = Vector2.zero;



        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_isGrounded)
            {
                _gravityVelocity = _jumpForse * Vector2.up;
                _jumpCount = 1;
            }
            else if (_jumpCount > 0 && _jumpCount < _maxJumpCount)
            {
                _gravityVelocity = _jumpForse * Vector2.up;
                _jumpCount++;
            }
        }


        _targetVelocity = _gravityVelocity + _surfaseVelocity;

        RaycastHit2D[] hits = new RaycastHit2D[1];
        int hitCount = _collider.Cast(_targetVelocity, _filter,hits, _targetVelocity.magnitude * Time.deltaTime);

        Debug.DrawRay(transform.position, _targetVelocity, Color.white);


        if (hitCount > 0)
        {
            //Debug.Log(hits[0].collider.gameObject);


            Vector2 surfaseNormal = hits[0].normal;
            Vector2 survaseAlong = new Vector2(surfaseNormal.y, -1 * surfaseNormal.x);

            Debug.DrawRay(transform.position, survaseAlong, Color.red, 1);

            if (surfaseNormal.y > _groundMinNormalY)                            // => it's wall;
            {
                _surfaseVelocity = survaseAlong * _surfaseVelocity.x;
            }
            else if (surfaseNormal.y < -1 * _groundMinNormalY)                  // => it's ceiling;
            {
                _surfaseVelocity = -1 * survaseAlong * _surfaseVelocity.x;

                if (_gravityVelocity.y > 0)
                    _gravityVelocity.y = 0;
            }
            else                                                                // => it's ground;
            {
                _surfaseVelocity.x = 0;
            }

            _targetVelocity = _gravityVelocity + _surfaseVelocity;


            Debug.DrawRay(transform.position, _targetVelocity, Color.blue);
        }

        Vector2 move = _targetVelocity * Time.deltaTime;

        hitCount = _collider.Cast(move, _filter,hits, move.magnitude);

        if (hitCount > 0)
            move = move.normalized * (hits[0].distance - 0.05f);

        transform.Translate(move);
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
}
