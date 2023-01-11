using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Animator))]
public class Gates : MonoBehaviour
{
    [SerializeField] private string _id;

    private const string OpenAnimation = "Open";
    private Animator _animator;
    private bool _isOpen;

    private void OnEnable()
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
            SaveLoadManager.SetBool(_id, true);
        }
    }
}