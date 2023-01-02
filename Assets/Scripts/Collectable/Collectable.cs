using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public abstract class Collectable : MonoBehaviour
{
    [SerializeField] private AudioSource _collectedSound;

    protected const string CollectedAnimation = "Collected";
    protected Animator Animator;
    protected bool Collected;

    private void Start()
    {
        Animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (Collected == false && collision.collider.TryGetComponent(out Player player))
        {
            if (_collectedSound.isPlaying == false)
                _collectedSound.Play();

            Collected = true;
            CollectByPlayer(player);
            StartCoroutine(Vanish());
        }
    }

    protected abstract void CollectByPlayer(Player player);

    private IEnumerator Vanish()
    {
        Animator.Play(CollectedAnimation);
        yield return new WaitForEndOfFrame();
        yield return new WaitForSeconds(Animator.GetCurrentAnimatorStateInfo(1).length);
        Destroy(gameObject);
    }

    protected virtual void OnValidate()
    {
        GetComponent<Collider2D>().isTrigger = false;
    }
}
