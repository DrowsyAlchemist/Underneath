using UnityEngine;

public class GoldenKey : Item
{
    [SerializeField] private float _radius = 3;

    public bool TryOpenLock(Vector2 position)
    {
        if (FinishGates.GetDistance(position) < _radius)
        {
            FinishGates.OpenGates();
            return true;
        }
        return false;
    }
}
