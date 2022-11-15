using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField][Range(0, 1)] private float _speed;
    [SerializeField] private float _xOffset;
    [SerializeField] private float _yOffset;

    private void Update()
    {
        Vector2 targetPosition = _target.position + new Vector3(_xOffset, _yOffset);
        Vector2 newPosition = Vector2.Lerp(transform.position, targetPosition, _speed);

        transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);
    }
}
