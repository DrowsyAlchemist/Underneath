using UnityEngine;

public class AccessPoint : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private RectTransform _interfaceCanvas;

    private static AccessPoint _instance;

    public static bool HasInstance => _instance != null;
    public static Transform Transform => _instance.transform;
    public static Player Player => _instance._player;
    public static RectTransform InterfaceCanvas => _instance._interfaceCanvas;

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

    public static void SetActive(bool value)
    {
        _instance.gameObject.SetActive(value);
    }
}