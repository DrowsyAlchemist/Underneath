using UnityEngine;

public class CoinsSpawner : MonoBehaviour
{
    [SerializeField] private Coin _coinTemplate;
    [SerializeField] private float _maxForce = 6;
    [SerializeField] private float _minForce = 4;
    [SerializeField][Range(0, 90)] private float _maxAngle = 30;

    [SerializeField] private AudioSource _coinFlipSound;

    private static CoinsSpawner _instance;
    private float _coinsModifier = 1;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(gameObject);
    }

    public static void ModifyCoinsCount(float modifier)
    {
        if (modifier <= 0)
            throw new System.ArgumentOutOfRangeException();

        _instance._coinsModifier *= modifier;
    }

    public static void Spawn(int count, Vector2 position, bool useModifier)
    {
        _instance._coinFlipSound.Play();
        float coinsCount = count * (useModifier ? _instance._coinsModifier : 1f);

        for (int i = 0; i < coinsCount; i++)
            _instance.SpawnCoin(position);
    }

    private void SpawnCoin(Vector2 position)
    {
        Coin coin = Instantiate(_coinTemplate, position, Quaternion.identity, null);
        Vector2 force = Random.Range(_minForce, _instance._maxForce) * Vector2.up;
        float angle = Random.Range(-1 * _maxAngle, _maxAngle);
        force = Quaternion.Euler(0, 0, angle) * force;
        coin.AddForce(force);
    }
}
