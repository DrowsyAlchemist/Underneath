using System.Collections;
using UnityEngine;

public abstract class Potion : AffectingItem
{
    [SerializeField] private float _duration;

    public float Duration => _duration;

    protected void CancelAffectingWithDelay(Player player)
    {
        StartCoroutine(CancelAffecting(player, _duration));
    }

    private IEnumerator CancelAffecting(Player player, float delay)
    {
        yield return new WaitForSeconds(delay);

        if (IsAffecting)
            base.CancelEffect(player);
    }
}