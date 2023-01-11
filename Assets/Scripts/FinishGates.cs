using UnityEngine;

public class FinishGates : Gates
{
    private static FinishGates _instance;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(gameObject);
    }

    public static float GetDistance(Vector2 position)
    {
        return Vector2.Distance(position, _instance.transform.position);
    }

    public static void OpenGates()
    {
        _instance.Open();
    }
}
