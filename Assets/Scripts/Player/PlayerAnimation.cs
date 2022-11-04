using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimation : MonoBehaviour
{
    private const string IdleAnimation = "Idle";
    private const string RunAnimation = "Run";
    private const string InteractionAnimation = "Interaction";
    private const string JumpAnimation = "Jump";

    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void PlayIdle()
    {
        _animator.Play(IdleAnimation);
    }

    public void PlayRun()
    {
        _animator.Play(RunAnimation);
    }

    public void PlayJump()
    {
        _animator.Play(JumpAnimation);
    }

    public void PlayInteraction()
    {

    }

    public void PlayDead()
    {
        
    }
}
