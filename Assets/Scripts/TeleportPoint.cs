using UnityEngine;

public class TeleportPoint : MonoBehaviour
{
    [SerializeField] private string _targetLocationName;
    [SerializeField] private Vector2 _spawnPosition;

    //public bool IsDiscovered => PlayerPrefs.GetInt(_targetLocationName) == 1;
    public bool IsDiscovered => true;
    public string TargetLocationName => _targetLocationName;

    public static void SetAvailable(string scenename)
    {
        foreach (var point in FindObjectsOfType<TeleportPoint>())
        {
            if (point.TargetLocationName.Equals(scenename))
            {
                PlayerPrefs.SetInt(scenename, 1);
                PlayerPrefs.Save();
                return;
            }
        }
        throw new System.Exception("Scene is not found.");
    }

    public void Teleport()
    {
        SceneLoader.LoadScene(_targetLocationName, _spawnPosition);
    }
}
