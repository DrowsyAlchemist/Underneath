using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HealingStatue : MonoBehaviour
{
    [SerializeField] private float _healingSpeed = 1;
    [SerializeField] private ParticleSystem _healingEffect;
    [SerializeField] private AudioSource _healingSound;

    private Coroutine _coroutine;
    private BrightnessController _brightnessController;

    private void Start()
    {
        _brightnessController = GetComponent<BrightnessController>();
        _brightnessController.Unlit();
        enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            _healingSound.Play();
            enabled = true;
            _brightnessController.Lit();
            _healingEffect.Play();
            _coroutine = StartCoroutine(HealPlayer(player));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player _))
        {
            _healingSound.Stop();
            _healingEffect.Stop();
            StopCoroutine(_coroutine);
            _brightnessController.Unlit();
            enabled = false;
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
