using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimation : MonoBehaviour
{
    private const string IdleAnimation = "Idle";
    private const string RunAnimation = "Run";
    private const string InteractionAnimation = "Interaction";
    private const string JumpAnimation = "Jump";
    private const string KnockAnimation = "Knock";
    private const string StandUpAnimation = "StandUp";
    private const string InvulnerabilityAnimation = "IsInvulnerability";
    private const string MeleeAnimation = "Melee";
    private const string ShootAnimation = "Shoot";

    public Animator Animator { get; private set; }

    private void Awake()
    {
        Animator = GetComponent<Animator>();
    }

    public void PlayIdle()
    {
        Animator.Play(IdleAnimation);
    }

    public void PlayRun()
    {
        Animator.Play(RunAnimation);
    }

    public void PlayJump()
    {
        Animator.Play(JumpAnimation);
    }

    public void PlayInteraction()
    {
        Animator.Play(InteractionAnimation);
    }

    public void PlayKnock()
    {
        Animator.Play(KnockAnimation);
    }

    public void PlayStandUp()
    {
        Animator.Play(StandUpAnimation);
    }

    public void PlayInvulnerability()
    {
        Animator.SetBool(InvulnerabilityAnimation, true);
    }

    public void StopInvulnerability()
    {
        Animator.SetBool(InvulnerabilityAnimation, false);
    }

    public void PlayMelee()
    {
        Animator.Play(MeleeAnimation);
    }

    public void PlayShoot()
    {
        Animator.Play(ShootAnimation);
    }
}