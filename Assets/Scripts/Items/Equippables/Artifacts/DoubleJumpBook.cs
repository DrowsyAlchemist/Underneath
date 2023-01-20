using UnityEngine;

public class DoubleJumpBook : AffectingItem
{
    private const int ExtraJumps = 1;

    protected override void StartAffecting(Player player)
    {
        player.PlayerMovement.IncreaseJumpCount(ExtraJumps);
    }

    protected override void StopAffecting(Player player)
    {
        player.PlayerMovement.DecreaseJumpCount(ExtraJumps);
    }
}
