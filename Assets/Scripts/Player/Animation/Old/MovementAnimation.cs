using UnityEngine;

[RequireComponent(typeof(PlayerAnimation))]
public class MovementAnimation : MonoBehaviour
{
    private PlayerAnimation _playerAnimation;

    private void Start()
    {
        _playerAnimation = GetComponent<PlayerAnimation>();
    }

    public void AnimateByVelocityAndGrounded(Vector2 velocity, bool isGrounded)
    {
        if (isGrounded == false)
            _playerAnimation.PlayJump();
        else if (velocity.x == 0)
            _playerAnimation.PlayIdle();
        else
            _playerAnimation.PlayRun();

        TurnByVelocity(velocity);
    }

    private void TurnByVelocity(Vector2 velocity)
    {
        if (velocity.x != 0)
            transform.LookForwardDirection(velocity);
    }
}
