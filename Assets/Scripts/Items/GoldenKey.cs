using UnityEngine;

public class GoldenKey : Item
{
    public bool TryOpenGates(Player player)
    {
        return FinishGates.TryOpenGates(player);
    }
}