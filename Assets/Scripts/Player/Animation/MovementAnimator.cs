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
            _playerAnimation.PlayJump();
        else if (Mathf.Abs(velocity.x) > Time.deltaTime)
            _playerAnimation.PlayRun();
        else
            _playerAnimation.PlayIdle();
    }

    private void TurnByVelocity(Vector2 velocity)
    {
        if (Mathf.Abs(velocity.x) > Time.deltaTime)
            transform.LookForwardDirection(velocity);
    }
}
