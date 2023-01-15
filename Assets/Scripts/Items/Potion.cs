using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public abstract class Potion : Item
{
    [SerializeField] private float _duration;

    public float Duration => _duration;

    public event UnityAction AffectingFinished;

    public void Drink(Player player)
    {
        var instance = Instantiate(this, player.transform);
        StartAffecting(player);
        instance.StartCoroutine(instance.CancelAffectingAndDestroy(player));
    }

    protected abstract void StartAffecting(Player player);
    protected abstract void StopAffecting(Player player);

    private IEnumerator CancelAffectingAndDestroy(Player player)
    {
        yield return new WaitForSeconds(_duration);
        StopAffecting(player);
        AffectingFinished?.Invoke();
        Destroy(gameObject);
    }
}