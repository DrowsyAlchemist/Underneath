using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] private Collider2D _collider;
    [SerializeField] private LayerMask _layerMask;

    public bool IsGrounded { get; private set; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.layer & (1 << _layerMask)) > 0)
        {
            Debug.Log("Ground");
            IsGrounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((collision.gameObject.layer & (1 << _layerMask)) > 0)
        {
            Debug.Log("OffGround");
            IsGrounded = false;
        }
    }
}
