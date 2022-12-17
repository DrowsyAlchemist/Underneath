using UnityEngine.Events;

public interface ITemporaryEffect
{
    public ItemData ItemData { get; }

    public event UnityAction<ITemporaryEffect> AffectingFinished;
}
