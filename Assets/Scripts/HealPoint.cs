using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(BrightnessController))]
public class HealPoint : MonoBehaviour
{
    [SerializeField] private float _healingSpeed = 1;
    private Coroutine _coroutine;
    private BrightnessController _lightControl;

    private void Start()
    {
        _lightControl = GetComponent<BrightnessController>();
        _lightControl.Unlit();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            _coroutine = StartCoroutine(HealPlayer(player));
            _lightControl.Lit();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player _))
        {
            StopCoroutine(_coroutine);
            _lightControl.Unlit();
        }
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
