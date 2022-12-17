using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public abstract class Potion : Item, ITemporaryEffect
{
    [SerializeField] private float _duration;

    public event UnityAction<ITemporaryEffect> AffectingFinished;

    public void Drink(Player player, Transform viewContainer = null)
    {
        var instance = Instantiate(this, null);
        StartAffecting(player);
        instance.StartCoroutine(CancelAffectingAndDestroy(instance, player));

        if (viewContainer != null)
            ActivePotionView.Create(instance, viewContainer);
    }

    protected abstract void StartAffecting(Player player);
    protected abstract void StopAffecting(Player player);

    private IEnumerator CancelAffectingAndDestroy(Potion instance, Player player)
    {
        yield return new WaitForSeconds(_duration);
        StopAffecting(player);
        instance.AffectingFinished?.Invoke(instance);
        Destroy(instance.gameObject);
    }
}
