using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class GroundChecker : MonoBehaviour
{
    [SerializeField] private float _groundDistanse = 0.03f;
    [SerializeField] private float _positionYModifier = 2;
    [SerializeField] private ContactFilter2D _filter;

    private Rigidbody2D _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public bool IsGrounded()
    {
        RaycastHit2D[] hit = new RaycastHit2D[1];
        int hitCount = _rigidbody.Cast(Vector2.down, _filter, hit, _groundDistanse);

        if (hitCount > 0)
            return hit[0].point.y - (transform.position.y + _groundDistanse * _positionYModifier) < 0;
        else
            return false;
        //return hitCount > 0;
    }
}
