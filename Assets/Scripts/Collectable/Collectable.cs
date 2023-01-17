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
    private Collider2D _collider;

    protected virtual void Start()
    {
        if (_collectedSound == null)
            throw new System.Exception("Audio sourse is not assigned.");

        _animator = GetComponent<Animator>();
        _collider = GetComponent<Collider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out Player player))
        {
            _collider.enabled = false;

            if (_collectedSound.isPlaying == false)
                _collectedSound.Play();

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
        gameObject.SetActive(false);
    }
}