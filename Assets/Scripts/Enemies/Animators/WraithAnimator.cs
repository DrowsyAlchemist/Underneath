using UnityEngine;

public class WraithAnimator : EnemyAnimator
{
    protected const string CastAnimation = "CastSpell";

    public virtual void PlayCastSpell()
    {
        Animator.Play(CastAnimation);
    }
}
