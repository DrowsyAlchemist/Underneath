using UnityEngine;

public class AccessPoint : MonoBehaviour
{
    [SerializeField] private Player _player;

    private static AccessPoint _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static Player Player => _instance._player;
}