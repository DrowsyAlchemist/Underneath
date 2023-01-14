using UnityEngine;

public class GoldenKey : Item
{
    [SerializeField] private float _radius = 3;

    public bool CanOpen(Vector2 position)
    {
        return FinishGates.GetDistance(position) < _radius;
    }

    public bool TryOpenLock(Vector2 position)
    {
        if (CanOpen(position))
        {
            FinishGates.OpenGates();
            return true;
        }
        return false;
    }

    public void OpenLock(Vector2 position)
    {
        if (CanOpen(position))
            FinishGates.OpenGates();
        else
            throw new System.InvalidOperationException();
    }
}