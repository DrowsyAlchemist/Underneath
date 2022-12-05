using UnityEngine;

public class DoubleJumpBook : PermanentEffectItem
{
    [SerializeField] private int _extraJumps;

    public override void Use(AccessPoint game)
    {
       game.Player.PlayerMovement.IncreaseJumpCount(_extraJumps);
    }
}
