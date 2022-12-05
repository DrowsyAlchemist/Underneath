using UnityEngine;

public class IceGolemAnimator : EnemyAnimator
{
    private const string ThrowAnimation = "Throw";

    public void PlayThrow()
    {
        Animator.Play(ThrowAnimation);
    }
}
