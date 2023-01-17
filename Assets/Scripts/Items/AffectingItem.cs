using UnityEngine.Events;

public abstract class AffectingItem : Item
{
    public bool IsAffecting { get; private set; }

    public event UnityAction AffectingFinished;

    public void ApplyEffect(Player player)
    {
        if (IsAffecting == false)
        {
            StartAffecting(player);
            IsAffecting = true;
        }
        else
        {
            throw new System.InvalidOperationException("Item is already affecting.");
        }
    }

    public void CancelEffect(Player player)
    {
        if (IsAffecting)
        {
            StopAffecting(player);
            IsAffecting = false;
            AffectingFinished?.Invoke();
        }
        else
        {
            throw new System.InvalidOperationException("Item is not affecting.");
        }
    }

    protected abstract void StartAffecting(Player player);
    protected abstract void StopAffecting(Player player);
}