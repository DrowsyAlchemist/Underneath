using UnityEngine;

[RequireComponent(typeof(PlayerAnimation))]
[RequireComponent(typeof(SpriteRenderer))]
public class MovementAnimator : MonoBehaviour
{
    private const float Delta = 0.01f;
    private PlayerAnimation _playerAnimation;
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _playerAnimation = GetComponent<PlayerAnimation>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void AnimateByVelocityAndGrounded(Vector2 velocity, bool grounded)
    {
        TurnByVelocity(velocity);

        if (grounded == false)
            _playerAnimation.PlayJump();
        else if (Mathf.Abs(velocity.x) > Delta)
            _playerAnimation.PlayRun();
        else
            _playerAnimation.PlayIdle();
    }

    private void TurnByVelocity(Vector2 velocity)
    {
        if (Mathf.Abs(velocity.x) > Delta)
            _spriteRenderer.flipX = (velocity.x < 0);
    }
}
