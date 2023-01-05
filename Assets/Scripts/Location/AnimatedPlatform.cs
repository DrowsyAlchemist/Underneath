using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimatedPlatform : MonoBehaviour
{
    private const string FallingAnimation = "Falling";
    private const string LiftAnimation = "Lift";

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void PlayFall()
    {
        _animator.Play(FallingAnimation);
    }

    public void PlayLift()
    {
        //if (_animator.GetCurrentAnimatorStateInfo(0).IsName(LiftAnimation) == false)
            _animator.Play(LiftAnimation);
    }
}
