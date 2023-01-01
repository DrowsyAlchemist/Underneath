using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyAnimator : MonoBehaviour
{
    protected const string IdleAnimation = "Idle";
    protected const string HurtAnimation = "Hurt";
    protected const string DieAnimation = "Die";
    protected const string AttackAnimation = "Attack";
    protected const string TauntAnimation = "Taunt";
    protected const string WalkAnimation = "Walk";

    public Animator Animator { get; private set; }

    private void Awake()
    {
        Animator = GetComponent<Animator>();
    }

    public virtual void PlayAttack()
    {
        Animator.Play(AttackAnimation);
    }

    public virtual void PlayIdle()
    {
        Animator.Play(IdleAnimation);
    }

    public virtual void PlayHurt()
    {
        Animator.Play(HurtAnimation);
    }

    public virtual void PlayDie()
    {
        Animator.Play(DieAnimation);
    }

    public virtual void PlayTaunt()
    {
        Animator.Play(TauntAnimation);
    }

    public virtual void PlayWalk()
    {
        Animator.Play(WalkAnimation);
    }
}
