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

    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (IsWindowOpen() == false)
            {
                _teleportWindow.gameObject.SetActive(true);
                Time.timeScale = 0;
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsWindowOpen())
            {
                _teleportWindow.gameObject.SetActive(false);
                Time.timeScale = 1;
            }
        }
    }

    private bool IsWindowOpen()
    {
        return _teleportWindow.gameObject.activeSelf;
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
