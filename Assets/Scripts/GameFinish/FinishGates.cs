using UnityEngine;

public class FinishGates : Gates
{
    [SerializeField] private float _openRadius = 3;

    private static FinishGates _instance;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(gameObject);
    }

    public static bool TryOpenGates(Player player)
    {
        if (IsPlayerCanOpen(player))
        {
            _instance.Open();
            return true;
        }
        return false;
    }

    private static bool IsPlayerCanOpen(Player player)
    {
        if (_instance)
            return Vector2.Distance(player.GetPosition(), _instance.transform.position) < _instance._openRadius;
        else
            return false;
    }
}