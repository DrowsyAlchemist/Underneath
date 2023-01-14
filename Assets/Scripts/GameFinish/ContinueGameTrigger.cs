using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ContinueGameTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.TryGetComponent(out Player player))
        {
            player.PlayerMovement.AllowInpupControl(true);
            player.transform.SetParent(AccessPoint.Transform);
        }
    }

    private void OnValidate()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }
}
