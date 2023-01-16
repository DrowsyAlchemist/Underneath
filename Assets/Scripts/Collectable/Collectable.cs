using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public abstract class Collectable : MonoBehaviour
{
    [SerializeField] private AudioSource _collectedSound;

    private const string CollectedAnimation = "Collected";
    private const int CollectedAnimationLayerIndex = 1;
    private Animator _animator;
    private bool _isCollected;

    private void Start()
    {
        if (_collectedSound == null)
            throw new System.Exception("Audio sourse is not assigned.");

        _animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_isCollected == false && collision.collider.TryGetComponent(out Player player))
        {
            if (_collectedSound.isPlaying == false)
                _collectedSound.Play();

            _isCollected = true;
            CollectByPlayer(player);
            StartCoroutine(Vanish());
        }
    }

    protected abstract void CollectByPlayer(Player player);

    private IEnumerator Vanish()
    {
        _animator.Play(CollectedAnimation);
        yield return new WaitForEndOfFrame();
        float delay = _animator.GetCurrentAnimatorStateInfo(CollectedAnimationLayerIndex).length;
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}