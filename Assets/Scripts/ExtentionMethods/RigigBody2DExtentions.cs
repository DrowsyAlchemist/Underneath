using UnityEngine;

public static class RigigBody2DExtentions
{
    public static void SetVelosityX(this Rigidbody2D rigidbody, float value)
    {
        rigidbody.velocity = new Vector2(value, rigidbody.velocity.y);
    }

    public static void SetVelosityY(this Rigidbody2D rigidbody, float value)
    {
        rigidbody.velocity = new Vector2(rigidbody.velocity.x, value);
    }
}
