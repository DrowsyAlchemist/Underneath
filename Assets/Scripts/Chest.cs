using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CoinsSpawner))]
public class Chest : MonoBehaviour, ITakeDamage
{
    [SerializeField] private string _id;
    [SerializeField] private int _coinsCount;

    private const string OpenAnimation = "Open";
    private Animator _animator;
    private bool _isOpened;

    private void Start()
    {
        PlayerPrefs.DeleteAll();

        _animator = GetComponent<Animator>();
        _isOpened = PlayerPrefs.GetInt(_id, defaultValue: 0) == 1;

        if (_isOpened)
            _animator.Play(OpenAnimation);
    }

    public void TakeDamage(int damage, Vector3 attackerPosition)
    {
        if (_isOpened == false)
        {
            _isOpened = true;
            PlayerPrefs.SetInt(_id, 1);
            Open();
        }
    }

    private void Open()
    {
        _animator.Play(OpenAnimation);
        GetComponent<CoinsSpawner>().Spawn(_coinsCount, transform.position);
    }

    private void OnValidate()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }
}
