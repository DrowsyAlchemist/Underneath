using UnityEngine;
using UnityEngine.Events;

public class TeleportPoint : MonoBehaviour
{
    [SerializeField] private string _targetLocationName;
    [SerializeField] private Vector2 _spawnPosition;

    private const string SavesFolderName = "TeleportPoints";

    public bool IsAvailable => SaveLoadManager.GetLoadOrDefault<bool>(SavesFolderName, _id);
    public string TargetLocationName => _targetLocationName;
    private string _id => _targetLocationName + "Point";

    public event UnityAction<TeleportPoint> BecameAvailable;

    public void Teleport()
    {
        SceneLoader.LoadScene(_targetLocationName, _spawnPosition);
    }

    public void SetAvailable()
    {
        if (IsAvailable == false)
        {
            SaveLoadManager.Save(SavesFolderName, _id, true);
            BecameAvailable?.Invoke(this);
        }
    }
}