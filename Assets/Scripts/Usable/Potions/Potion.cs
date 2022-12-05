using UnityEngine;

public abstract class Potion : UseableItem
{
    [SerializeField] private float _duration;

    public float Duration => _duration;
}
