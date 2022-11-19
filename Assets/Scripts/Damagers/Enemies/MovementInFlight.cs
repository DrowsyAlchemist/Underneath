using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MovementInFlight : MonoBehaviour
{
    [SerializeField] private LayerMask _obstacles;

    public void MoveRelativeTarget(Vector3 targetPosition, float speed, bool positiveDirection = true)
    {
        Vector2 targetLocalPosition = targetPosition - transform.position;
        Vector2 direction = (positiveDirection ? 1 : -1) * targetLocalPosition.normalized;
        float step = speed * Time.deltaTime;

        //...
        transform.Translate(step * direction);
    }
}
