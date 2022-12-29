using UnityEngine;

[RequireComponent(typeof(Animator))]
public class SceneLoadAnimator : MonoBehaviour
{
    private const string LoadAnimation = "Load";
    private const string UnloadAnimation = "Unload";

    private Animator _animator;

    public float CurrentAnimationLength => _animator.GetCurrentAnimatorStateInfo(0).length;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void PlayLoad()
    {
        _animator.Play(LoadAnimation);
    }

    public void PlayUnload()
    {
        _animator.Play(UnloadAnimation);
    }
}
