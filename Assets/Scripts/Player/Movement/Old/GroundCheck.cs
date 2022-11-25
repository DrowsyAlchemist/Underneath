using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class GroundCheck : MonoBehaviour
{
    [SerializeField] private LayerMask _groundLayer;

    private Rigidbody2D _rigidbody;
    private ContactFilter2D _contactFilter = new ContactFilter2D();

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _contactFilter.useLayerMask = true;
        _contactFilter.layerMask = _groundLayer;
    }

    public bool IsGrounded(float groundDistanse)
    {
        RaycastHit2D[] hit = new RaycastHit2D[1];
        int hitCount = _rigidbody.Cast(Vector2.down, _contactFilter, hit, groundDistanse);
        return hitCount > 0;
    }
}
