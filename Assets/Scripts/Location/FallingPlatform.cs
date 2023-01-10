using UnityEngine;

[RequireComponent(typeof(Animator))]
public class FallingPlatform : MonoBehaviour
{
    [SerializeField] private string _id;

    private const string FallingAnimation = "Falling";

    private bool _isFallen;
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _isFallen = SaveLoadManager.GetBoolOrDefault(_id);

        if (_isFallen)
            _animator.Play(FallingAnimation);
    }

    public void PlayFall()
    {
        _animator.Play(FallingAnimation);
        _isFallen = true;
        SaveLoadManager.SetBool(_id, true);
    }
}
