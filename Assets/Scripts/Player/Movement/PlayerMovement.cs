using UnityEngine;

public class PlayerMovement : PhysicMovement
{
    private bool _canInputControlled = true;

    protected override void Update()
    {
        if (_canInputControlled)
            GetInputVelocity();
        else
            Velocity.x = 0;

        base.Update();
    }


    public void AllowInpupControl(bool isAllowed)
    {
        _canInputControlled = isAllowed;
    }

    private void GetInputVelocity()
    {
        if (Input.GetKey(KeyCode.D))
            Velocity.x = Speed;
        else if (Input.GetKey(KeyCode.A))
            Velocity.x = -1 * Speed;
        else
            Velocity.x = 0;

        if (Input.GetKeyDown(KeyCode.Space))
            Jump();
    }
}