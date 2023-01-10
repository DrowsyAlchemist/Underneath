using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class GameFinishTrigger : MonoBehaviour
{
    [SerializeField] private LiftPlatform _platform;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.TryGetComponent(out Player player))
        {
            player.PlayerMovement.AllowInpupControl(false);
            _platform.LiftPlayer(player.transform);
        }
    }

    private void OnValidate()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }
}
