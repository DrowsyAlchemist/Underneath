using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HealPoint : MonoBehaviour
{
    [SerializeField] private float _healingSpeed = 1;

    private Coroutine _coroutine;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
            _coroutine = StartCoroutine(HealPlayer(player));
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player _))
            if (_coroutine != null)
                StopCoroutine(_coroutine);
    }

    private IEnumerator HealPlayer(Player player)
    {
        while (enabled)
        {
            yield return new WaitForSeconds(_healingSpeed);
            player.PlayerHealth.RestoreHealth(1);
        }
    }

    private void OnValidate()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }
}
