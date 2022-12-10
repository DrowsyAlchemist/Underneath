using UnityEngine;

[RequireComponent(typeof(Animator))]
public class SceneLoadAnimator : MonoBehaviour
{
    private const string LoadAnimation = "Load";
    private const string UnloadAnimation = "UnLoad";
    private Animator _animator;

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
