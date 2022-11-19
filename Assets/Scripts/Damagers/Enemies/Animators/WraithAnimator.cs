using UnityEngine;

public class WraithAnimator : EnemyAnimator
{
    private const string CastAnimation = "CastSpell";

    public virtual void PlayCastSpell()
    {
        Animator.Play(CastAnimation);
    }
}
