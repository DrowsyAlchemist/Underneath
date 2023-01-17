using UnityEngine;

[RequireComponent(typeof(Animator))]
public class FallingPlatform : MonoBehaviour, ISaveable
{
    [SerializeField] private string _id;

    private const string SavesFolderName = "Platforms";
    private const string FallingAnimation = "Falling";

    private bool _isFallen;
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _isFallen = SaveLoadManager.GetLoadOrDefault<bool>(SavesFolderName, _id);

        if (_isFallen)
            _animator.Play(FallingAnimation);
    }

    public void PlayFall()
    {
        _animator.Play(FallingAnimation);
        _isFallen = true;
    }

    public void Save()
    {
        SaveLoadManager.Save(SavesFolderName, _id, _isFallen);
    }
}
