using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class GroundChecker : MonoBehaviour
{
    [SerializeField] private LayerMask _ground;

    public bool IsGrounded { get; private set; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((1 << collision.gameObject.layer & _ground) > 0)
        {
            IsGrounded = true;
            PlayerSounds.PlayLanding();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((1 << collision.gameObject.layer & _ground) > 0)
            IsGrounded = false;
    }

    private void OnValidate()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }
}