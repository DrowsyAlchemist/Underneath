using UnityEngine;
using UnityEngine.Events;

public class TeleportPoint : MonoBehaviour, ISaveable
{
    [SerializeField] private string _targetLocationName;
    [SerializeField] private Vector2 _spawnPosition;

    public bool IsAvailable { get; private set; }
    public string TargetLocationName => _targetLocationName;
    private string _id => _targetLocationName + "Point";

    public event UnityAction<TeleportPoint> BecameAvailable;

    private void Awake()
    {
        IsAvailable = SaveLoadManager.GetBoolOrDefault(_id);
    }

    public void Teleport()
    {
        SceneLoader.LoadScene(_targetLocationName, _spawnPosition);
    }

    public void SetAvailable()
    {
        if (IsAvailable == false)
        {
            IsAvailable = true;
            BecameAvailable?.Invoke(this);
        }
    }

    public void Save()
    {
        SaveLoadManager.SetBool(_id, true);
    }
}
