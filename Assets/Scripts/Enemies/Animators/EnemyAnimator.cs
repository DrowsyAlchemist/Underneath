using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyAnimator : MonoBehaviour
{
    private const string IdleAnimation = "Idle";
    private const string HurtAnimation = "Hurt";
    private const string DieAnimation = "Die";
    private const string AttackAnimation = "Attack";
    private const string TauntAnimation = "Taunt";
    private const string WalkAnimation = "Walk";

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