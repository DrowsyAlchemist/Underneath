using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(BrightnessController))]
public class HealStatue : MonoBehaviour
{
    [SerializeField] private string _targetSceneName = "Village";
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private float _healingSpeed = 1;
    private Coroutine _coroutine;
    private BrightnessController _lightControl;

    private Player _player;

    private void Start()
    {
        _lightControl = GetComponent<BrightnessController>();
        _lightControl.Unlit();
        enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            enabled = true;
            _lightControl.Lit();
            _player = player;
            _coroutine = StartCoroutine(HealPlayer(player));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player _))
        {
            StopCoroutine(_coroutine);
            _lightControl.Unlit();
            enabled = false;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            _player.transform.position = _spawnPoint.position;
            SceneManager.LoadScene(_targetSceneName);
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
