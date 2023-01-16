using UnityEngine;

public class AccessPoint : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private RectTransform _interfaceCanvas;

    private static AccessPoint _instance;

    public static Transform Transform => _instance.transform;
    public static Player Player => _instance._player;
    public static RectTransform InterfaceCanvas => _instance._interfaceCanvas;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
            gameObject.SetActive(false);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static void SetEnable(bool value)
    {
        _instance.gameObject.SetActive(value);
    }
}