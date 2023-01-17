using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class TeleportArea : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            enabled = true;
            string message = "Press \"T\" to open available teleport points.";
            MessageCreator.ShowMessage(message, AccessPoint.InterfaceCanvas, MessageType.Tip);
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