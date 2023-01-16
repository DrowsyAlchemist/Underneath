using UnityEngine;

public class DoubleJumpBook : AffectingItem
{
    [SerializeField] private int _extraJumps;

    protected override void StartAffecting(Player player)
    {
        player.PlayerMovement.IncreaseJumpCount(_extraJumps);
    }

    protected override void StopAffecting(Player player)
    {
        player.PlayerMovement.DecreaseJumpCount(_extraJumps);
    }
}
