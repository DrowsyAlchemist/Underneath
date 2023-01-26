using System;

public abstract class AffectingItem : Item
{
    public bool IsAffecting { get; private set; }

    public event Action<AffectingItem> AffectingFinished;

    public void SetAffecting()
    {
        if (IsAffecting == false)
            IsAffecting = true;
        else
            throw new System.InvalidOperationException("Item is already affecting.");
    }

    public void ApplyEffect(Player player)
    {
        SetAffecting();
        StartAffecting(player);
    }

    public void CancelEffect(Player player)
    {
        if (IsAffecting)
        {
            StopAffecting(player);
            IsAffecting = false;
            AffectingFinished?.Invoke(this);
        }
        else
        {
            throw new System.InvalidOperationException("Item is not affecting.");
        }
    }

    protected abstract void StartAffecting(Player player);
    protected abstract void StopAffecting(Player player);
}