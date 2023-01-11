using UnityEngine.Events;

public interface ITemporaryEffect
{
    public ItemData Data { get; }

    public event UnityAction<ITemporaryEffect> AffectingFinished;
}
