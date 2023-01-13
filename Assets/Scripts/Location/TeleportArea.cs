using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class TeleportArea : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player _))
        {
            enabled = false;
        }
    }

    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.T))
            Teleport.ShowWindow();

        if (Input.GetKeyDown(KeyCode.Escape))
            Teleport.HideWindow();
    }

    private void OnValidate()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }
}
