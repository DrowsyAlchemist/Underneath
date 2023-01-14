using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public abstract class Potion : Item
{
    [SerializeField] private float _duration;

    public event UnityAction<Potion> AffectingFinished;

    public void Drink(Player player)
    {
        var instance = Instantiate(this, null);
        StartAffecting(player);
        instance.StartCoroutine(CancelAffectingAndDestroy(instance, player));
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
