using System;
using UnityEngine;
using UnityEngine.UI;

public class ActivePotionRenderer : MonoBehaviour
{
    [SerializeField] private Image _image;

    public Potion Potion { get; private set; }

    public event Action<ActivePotionRenderer> Destroying;

    public void Render(Potion potion)
    {
        Potion = potion;
        _image.sprite = potion.Data.Sprite;
        potion.transform.SetParent(transform);
        potion.AffectingFinished += OnPotionStopAffecting;
    }

    private void OnPotionStopAffecting(AffectingItem affectingItem)
    {
        affectingItem.AffectingFinished -= OnPotionStopAffecting;
        Destroying?.Invoke(this);
        Destroy(gameObject);
    }
}