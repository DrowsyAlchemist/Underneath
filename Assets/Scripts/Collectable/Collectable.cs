using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Animator))]
public abstract class Collectable : MonoBehaviour
{
    protected const string CollectedAnimation = "Collected";
    protected Animator Animator;
    protected bool Collected;

    private void Start()
    {
        Animator = GetComponent<Animator>();
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (Collected == false && collision.TryGetComponent(out Player player))
        {
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
        GetComponent<Collider2D>().isTrigger = true;
    }
}
