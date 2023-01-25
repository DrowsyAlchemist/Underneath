using UnityEngine;

[RequireComponent(typeof(PlayerAnimation))]
public class MovementAnimator : MonoBehaviour
{
    private PlayerAnimation _playerAnimation;

    private void Start()
    {
        _playerAnimation = GetComponent<PlayerAnimation>();
    }

    public void AnimateByVelocityAndGrounded(Vector2 velocity, bool grounded)
    {
        TurnByVelocity(velocity);

        if (grounded == false)
        {
            PlayerSounds.PlayJumpLoop();
            _playerAnimation.PlayJump();
        }
        else if (Mathf.Abs(velocity.x) > Time.deltaTime)
        {
            PlayerSounds.PlayRun();
            _playerAnimation.PlayRun();
        }
        else
        {
            PlayerSounds.PlayIdle();
            _playerAnimation.PlayIdle();
        }
    }

    private void TurnByVelocity(Vector2 velocity)
    {
        if (Mathf.Abs(velocity.x) > Time.deltaTime)
            transform.LookForwardDirection(velocity);
    }
}