using UnityEngine;

public class WraithAnimation : EnemyAnimation
{
    private const string HitAnimation = "Hit";

    public void PlayHit()
    {
        Animator.Play(HitAnimation);
    }
}
