using UnityEngine;

public class PlayerFollowingCamera : MonoBehaviour
{
    [SerializeField][Range(0, 1)] private float _speed = 0.03f;
    [SerializeField] private float _xOffset = 0;
    [SerializeField] private float _yOffset = 2;

    private Transform _followingTarget;

    private void Start()
    {
        _followingTarget = AccessPoint.Player.transform;
    }

    private void Update()
    {
        Vector2 targetPosition = _followingTarget.position + new Vector3(_xOffset, _yOffset);
        Vector2 newPosition = Vector2.Lerp(transform.position, targetPosition, _speed);
        transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);
    }
}