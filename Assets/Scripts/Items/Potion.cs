using System.Collections;
using UnityEngine;

public abstract class Potion : Item
{
    [SerializeField] private float _duration;

    public void Drink(Player player)
    {
        StartAffecting(player);
        StartCoroutine(CancelAffectingAndDestroy(player));
    }

    protected abstract void StartAffecting(Player player);
    protected abstract void StopAffecting(Player player);

    private IEnumerator CancelAffectingAndDestroy(Player player)
    {
        yield return new WaitForSeconds(_duration);
        StopAffecting(player);

        if (transform.parent.TryGetComponent(out ItemRenderer itemRenderer))
            Destroy(itemRenderer.gameObject);
        else
            Destroy(gameObject);
    }
}
