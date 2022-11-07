using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Collider2D))]
public class Coin : MonoBehaviour
{
    private const string CollectedAnimation = "Collected";

    private Animator _animator;
    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Player player))
        {
            player.TakeCoin();
            StartCoroutine(Vanish());
        }
    }

    private IEnumerator Vanish()
    {
        _animator.Play(CollectedAnimation);
        yield return new WaitForEndOfFrame();
        yield return new WaitForSeconds(_animator.GetCurrentAnimatorStateInfo(1).length);
        Destroy(gameObject);
    }

    private void OnValidate()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }
}
