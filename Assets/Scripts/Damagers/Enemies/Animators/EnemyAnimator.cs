using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyAnimator : MonoBehaviour
{
    protected const string IdleAnimation = "Idle";
    protected const string HurtAnimation = "Hurt";
    protected const string DieAnimation = "Die";
    protected const string AttackAnimation = "Attack";
    protected const string IdleBlinkAnimation = "IdleBlink";
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

    public void PlayIdle()
    {
        Animator.Play(IdleAnimation);
    }

    public void PlayHurt()
    {
        Animator.Play(HurtAnimation);
    }

    public void PlayDie()
    {
        Animator.Play(DieAnimation);
    }


    public void PlayIdleBlink()
    {
        Animator.Play(IdleBlinkAnimation);
    }

    public void PlayTaunt()
    {
        Animator.Play(TauntAnimation);
    }

    public void PlayWalk()
    {
        Animator.Play(WalkAnimation);
    }
}
