using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformExtentions 
{
    public static void LookForwardDirection(this Transform transform, bool positiveDirection)
    {
        int direction = positiveDirection ? 1 : -1;

        if (transform.localScale.x * direction < 0)
            transform.localScale = new Vector3(-1 * transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }
}
