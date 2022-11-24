using UnityEngine;

[RequireComponent(typeof(PlayerAnimation))]
[RequireComponent(typeof(Rigidbody2D))]
public class MovementAnimator : MonoBehaviour
{
    private const float Delta = 0.01f;
    private PlayerAnimation _playerAnimation;
    private Rigidbody2D _rigidBody;

    private void Start()
    {
        _playerAnimation = GetComponent<PlayerAnimation>();
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        AnimateByVelocity();
        TurnByVelocity();
    }

    private void AnimateByVelocity()
    {
        if (Mathf.Abs(_rigidBody.velocity.y) > Delta)
            _playerAnimation.PlayJump();
        else if (Mathf.Abs(_rigidBody.velocity.x) > Delta)
            _playerAnimation.PlayRun();
        else
            _playerAnimation.PlayIdle();
    }

    private void TurnByVelocity()
    {
        if (Mathf.Abs(_rigidBody.velocity.x) > Delta)
        {
            bool positiveDirection = _rigidBody.velocity.x > 0;
            transform.LookForwardDirection(positiveDirection);
        }
    }
}
