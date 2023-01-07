using UnityEngine;

public class GoldenKey : Item
{
    [SerializeField] private float _radius = 3;

    public bool TryOpenLock(Vector2 position)
    {
        if (FinishBars.GetDistance(position) < _radius)
        {
            FinishBars.OpenBars();
            return true;
        }
        return false;
    }
}
