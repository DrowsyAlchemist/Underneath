using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public abstract class Potion : Item
{
    [SerializeField] private float _duration;

    public float Duration => _duration;

    public void Drink(Player player)
    {
        var instance = Instantiate(this, player.transform);
        StartAffecting(player);
        instance.StartCoroutine(CancelEffect(player, instance.gameObject));
    }

    public abstract void StartAffecting(Player player);

    public abstract void StopAffecting(Player player);

    private IEnumerator CancelEffect(Player player, GameObject instance)
    {
        yield return new WaitForSeconds(_duration);
        StopAffecting(player);
        Destroy(instance);
    }
}
