using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(BrightnessController))]
public class Statue : MonoBehaviour
{
    [SerializeField] private float _healingSpeed = 1;
    [SerializeField] private ParticleSystem _healingEffect;
    [SerializeField] private AudioSource _healingSound;

    [SerializeField] private RectTransform _teleportWindow;

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
            TeleportPoint.SetAvailable(SceneManager.GetActiveScene().name);
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            bool isOpened = _teleportWindow.gameObject.activeSelf;
            _teleportWindow.gameObject.SetActive(isOpened == false);
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
