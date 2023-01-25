using UnityEngine;

public abstract class Potion : AffectingItem
{
    [SerializeField] private float _duration;

    private float _elapsedTime;

    public float Duration => _duration;

    protected override sealed void Awake()
    {
        base.Awake();
        enabled = false;
    }

    protected override sealed void StartAffecting(Player player)
    {
        enabled = true;
        Affect(player);
    }

    protected abstract void Affect(Player player);

    private void Update()
    {
        _elapsedTime += Time.deltaTime;

        if (_elapsedTime > _duration)
            base.CancelEffect(AccessPoint.Player);
    }

    private void OnDestroy()
    {
        if (IsAffecting)
            base.CancelEffect(AccessPoint.Player);
    }
}