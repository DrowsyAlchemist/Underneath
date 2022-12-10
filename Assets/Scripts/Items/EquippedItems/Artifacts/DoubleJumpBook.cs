using UnityEngine;

public class DoubleJumpBook : EquippableItem
{
    [SerializeField] private int _extraJumps;

    public override void Affect(Player player)
    {
        player.PlayerMovement.IncreaseJumpCount(_extraJumps);
    }

    public override void StopAffecting(Player player)
    {
        player.PlayerMovement.DecreaseJumpCount(_extraJumps);
    }
}
