using UnityEngine;

public class PlayerMovement : PhysicsMovement
{
    [SerializeField] private float _jumpForse = 5;
    [SerializeField] private int _maxJumpCount = 2;
    [SerializeField] private float _timeBeforeLanding = 0.08f;
    [SerializeField] private float _timeAflerGetOffGround = 0.15f;

    private bool _canInputControlled = true;
    private int _jumpsLeft;
    private bool _spaceIsPressed;
    private float _secondsInAir;
    private float _secondsAfterSpacePressed;

    private void OnEnable()
    {
        _spaceIsPressed = false;
    }

    public void AllowInpupControl(bool isAllowed)
    {
        _canInputControlled = isAllowed;
    }

    public void IncreaseJumpCount(int value)
    {
        if (value<0)
            throw new System.ArgumentOutOfRangeException("value");

        _maxJumpCount += value;
    }

    public void DecreaseJumpCount(int value)
    {
        _maxJumpCount -= value;
    }

    private void Update()
    {
        if (_canInputControlled)
            GetInputVelocity();
        else
            SetVelocityX(0);

        Move();
    }

    private void GetInputVelocity()
    {
        SmartJump();

        if (Input.GetKey(KeyCode.D))
            SetVelocityX(Speed);
        else if (Input.GetKey(KeyCode.A))
            SetVelocityX(-1 * Speed);
        else
            SetVelocityX(0);
    }

    private void SmartJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (GroundChecker.IsGrounded || (_secondsInAir < _timeAflerGetOffGround))
                JumpFromGround();
            else if (_jumpsLeft > 0)
                JumpInAir();
            else
                _spaceIsPressed = true;

            _secondsAfterSpacePressed = 0;
        }
        else if (GroundChecker.IsGrounded)
        {
            if (_spaceIsPressed && _secondsAfterSpacePressed < _timeBeforeLanding)
                JumpFromGround();
            else if (_secondsInAir > 0)
                _jumpsLeft = 0;

            _spaceIsPressed = false;
            _secondsInAir = 0;
        }
        else
        {
            _secondsInAir += Time.deltaTime;
            _secondsAfterSpacePressed += Time.deltaTime;
        }
    }

    private void JumpFromGround()
    {
        PlayerSounds.PlayJumpFromGround();
        Jump(_jumpForse);
        _jumpsLeft = _maxJumpCount - 1;
    }

    private void JumpInAir()
    {
        PlayerSounds.PlayJumpInAir();
        Jump(_jumpForse);
        _jumpsLeft--;
    }
}