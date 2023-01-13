using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Animator))]
public class Gates : MonoBehaviour, ISaveable
{
    [SerializeField] private string _id;

    private const string OpenAnimation = "Open";
    private Animator _animator;
    private bool _isOpen;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _isOpen = SaveLoadManager.GetBoolOrDefault(_id);

        if (_isOpen)
            _animator.Play(OpenAnimation);
    }

    public void Open()
    {
        if (_isOpen == false)
        {
            _animator.Play(OpenAnimation);
            _isOpen = true;
        }
    }

    public void Save()
    {
        SaveLoadManager.SetBool(_id, true);
    }
}