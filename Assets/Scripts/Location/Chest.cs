using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Animator))]
public class Chest : MonoBehaviour, ITakeDamage
{
    [SerializeField] private string _id;
    [SerializeField] private int _coinsCount;

    private const string OpenAnimation = "Open";
    private Animator _animator;
    private bool _isOpened;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _isOpened = SaveLoadManager.GetBoolOrDefault(_id);

        if (_isOpened)
            _animator.Play(OpenAnimation);
    }

    public void TakeDamage(int damage, Vector3 attackerPosition)
    {
        if (_isOpened == false)
        {
            Open();
            _isOpened = true;
            SaveLoadManager.SetBool(_id, true);
        }
    }

    private void Open()
    {
        _animator.Play(OpenAnimation);
        CoinsSpawner.Spawn(_coinsCount, transform.position, useModifier: false);
    }

    private void OnValidate()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }
}
