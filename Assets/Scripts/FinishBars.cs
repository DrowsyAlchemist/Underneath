using UnityEngine;

public class FinishBars : Bars
{
    private static FinishBars _instance;

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

    public static void OpenBars()
    {
        _instance.Open();
    }
}
