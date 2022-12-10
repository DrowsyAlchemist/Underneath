using UnityEngine;

public class CoinsSpawner : MonoBehaviour
{
    [SerializeField] private Coin _coinTemplate;
    [SerializeField] private float _maxForce = 6;
    [SerializeField] private float _minForce = 4;
    [SerializeField][Range(0, 90)] private float _maxAngle = 30;

    private float _coinsModifier = 1;

    public void ModifyCoinsCount(float modifier)
    {
        if (modifier <= 0)
            throw new System.ArgumentOutOfRangeException();

        _coinsModifier *= modifier;
    }

    public void Spawn(int count, Vector2 position)
    {
        for (int i = 0; i < count * _coinsModifier; i++)
        {
            Coin coin = Instantiate(_coinTemplate, position, Quaternion.identity, null);
            Vector2 force = Random.Range(_minForce, _maxForce) * Vector2.up;
            float angle = Random.Range(-1 * _maxAngle, _maxAngle);
            force = Quaternion.Euler(0, 0, angle) * force;
            coin.AddForce(force);
        }
    }
}
