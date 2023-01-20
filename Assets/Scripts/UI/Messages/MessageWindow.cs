using System.Collections;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public abstract class MessageWindow : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    private const string ShowAnimation = "Show";
    private const string CloseAnimation = "Close";

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnDisable()
    {
        Destroy(gameObject);
    }

    public void Show(string message)
    {
        _text.text = message;
        gameObject.SetActive(true);
        _animator.Play(ShowAnimation);
    }

    public void Close(float delay = 0)
    {
        StartCoroutine(DestroyAfterAnimation(delay));
    }

    private IEnumerator DestroyAfterAnimation(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        _animator.Play(CloseAnimation);
        yield return new WaitForEndOfFrame();
        yield return new WaitForSecondsRealtime(_animator.GetCurrentAnimatorStateInfo(0).length);
        Destroy(gameObject);
    }
}