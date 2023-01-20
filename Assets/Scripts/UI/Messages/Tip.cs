using UnityEngine;

public class Tip : MessageWindow
{
    [SerializeField] private float _delay = 2;

    private void OnEnable()
    {
        base.Close(_delay);
    }
}