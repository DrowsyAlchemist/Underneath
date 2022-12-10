using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PotionActivator : MonoBehaviour
{
    [SerializeField] private RectTransform _container;

    private Player _player;

    private void Start()
    {
        _player = FindObjectOfType<AccessPoint>().Player;
    }

    public void UsePotion(ItemRenderer potionRenderer)
    {
        if (potionRenderer.Item.TryGetComponent(out Potion potion) == false)
            throw new System.ArgumentException("Item should have component \"Potion.\"");

        potionRenderer.transform.SetParent(_container);
        var instanse = Instantiate(potion, potionRenderer.transform);
        instanse.StartAffecting(_player);
        Timer timer = instanse.gameObject.AddComponent<Timer>();
        timer.WentOff += OnEffectEnded;
        timer.StartTimer(instanse.Duration);
    }

    private void OnEffectEnded(Timer timer)
    {
        timer.WentOff -= OnEffectEnded;
        ItemRenderer itemRenderer = timer.transform.parent.GetComponent<ItemRenderer>();
        Potion potion = itemRenderer.Item as Potion;
        potion.StopAffecting(_player);
        Destroy(itemRenderer.gameObject);
    }

    private class Timer : MonoBehaviour
    {
        private Coroutine _coroutine;

        public event UnityAction<Timer> WentOff;

        public void StartTimer(float seconds)
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);

            _coroutine = StartCoroutine(Countdown(seconds));
        }

        private IEnumerator Countdown(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            WentOff?.Invoke(this);
        }
    }
}
