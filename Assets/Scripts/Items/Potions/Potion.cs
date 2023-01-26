using UnityEngine;

public abstract class Potion : AffectingItem
{
    [SerializeField] private float _duration;

    private float _elapsedTime;

    public float Duration => _duration;

    private void Update()
    {
        if (IsAffecting)
        {
            _elapsedTime += Time.deltaTime;

            if (_elapsedTime > _duration)
                CancelEffect(AccessPoint.Player);
        }
    }
}