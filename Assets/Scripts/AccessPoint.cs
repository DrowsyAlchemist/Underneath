using UnityEngine;

public class AccessPoint : MonoBehaviour
{
    [SerializeField] private Player _player;

    private static AccessPoint _instance;

    public static Transform Transform => _instance.transform;
    public static Player Player => _instance._player;

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

}