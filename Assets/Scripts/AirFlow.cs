using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class AirFlow : MonoBehaviour
{
    [SerializeField] private float _playerHorizontalSpeed;
    [SerializeField] private float _playerVerticalSpeed;

    private Player _player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            _player = player;
            player.PlayerMovement.enabled = false;
            player.PlayerAnimation.PlayJump();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
            InFlowMovement();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
            player.PlayerMovement.enabled = true;
    }

    private void InFlowMovement()
    {
        float horizontalVelocity = Input.GetAxis("Horizontal") * _playerHorizontalSpeed;
        Vector2 playerVelocity = new Vector2(horizontalVelocity, _playerVerticalSpeed);
        _player.transform.Translate(playerVelocity * Time.deltaTime);
    }

    private void OnValidate()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }
}
