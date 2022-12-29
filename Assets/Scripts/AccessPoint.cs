using UnityEngine;

public class AccessPoint : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private CoinsSpawner _coinsSpawner;
    [SerializeField] private SceneLoader _sceneLoader;
    [SerializeField] private SceneLoadAnimator _sceneLoadAnimator;

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
    public static CoinsSpawner CoinsSpawner => _instance._coinsSpawner;
}
