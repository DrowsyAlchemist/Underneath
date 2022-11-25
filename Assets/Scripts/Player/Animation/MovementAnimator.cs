using UnityEngine;

[RequireComponent(typeof(PlayerAnimation))]
public class MovementAnimator : MonoBehaviour
{
    private const float Delta = 0.01f;
    private PlayerAnimation _playerAnimation;

    private void Start()
    {
        _playerAnimation = GetComponent<PlayerAnimation>();
    }

    public void AnimateByVelocity(Vector2 velocity)
    {
        TurnByVelocity(velocity);

        if (Mathf.Abs(velocity.y) > Delta)
            _playerAnimation.PlayJump();
        else if (Mathf.Abs(velocity.x) > Delta)
            _playerAnimation.PlayRun();
        else
            _playerAnimation.PlayIdle();
    }

    private void TurnByVelocity(Vector2 velocity)
    {
        if (Mathf.Abs(velocity.x) > Delta)
        {
            bool positiveDirection = velocity.x > 0;
            transform.LookForwardDirection(positiveDirection);
        }
    }
}
