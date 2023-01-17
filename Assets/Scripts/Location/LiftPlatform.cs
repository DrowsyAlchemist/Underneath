using UnityEngine;

[RequireComponent(typeof(Animator))]
public class LiftPlatform : MonoBehaviour, ISaveable
{
    [SerializeField] private string _id;
    [SerializeField] private Transform _playerPoint;

    private const string SavesFolderName = "Platforms";
    private const string LiftAnimation = "Lift";

    private bool _isActivated;
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _isActivated = SaveLoadManager.GetLoadOrDefault<bool>(SavesFolderName, _id);

        if (_isActivated)
            _animator.Play(LiftAnimation);
    }

    public void LiftPlayer(Transform playerTransform)
    {
        playerTransform.SetParent(transform);
        playerTransform.position = _playerPoint.position;
        _animator.Play(LiftAnimation);
        _isActivated = true;
    }

    public void Save()
    {
        SaveLoadManager.Save(SavesFolderName, _id, true);
    }
}
