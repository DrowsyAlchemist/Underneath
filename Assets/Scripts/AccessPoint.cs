using UnityEngine;

public class AccessPoint : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private CoinsSpawner _coinsSpawner;

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

    public Player Player => _player;
    public CoinsSpawner CoinsSpawner => _coinsSpawner;
}
