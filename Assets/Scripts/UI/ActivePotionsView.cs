using System.Collections.Generic;
using UnityEngine;

public class ActivePotionsView : MonoBehaviour
{
    [SerializeField] private RectTransform _container;
    [SerializeField] private ActivePotionRenderer _activePotionRendererTemplate;

    private List<ActivePotionRenderer> _activePotionRenderers = new();

    public ActivePotions ActivePotions { get; private set; }

    private void Start()
    {
        ActivePotions = AccessPoint.Player.ActivePotions;
        ActivePotions.PotionAdded += OnPotionAdded;
        ActivePotions.Cleared += OnActivePotionsCleared;

        var activePotions = ActivePotions.GetActivePotions();

        foreach (var potion in activePotions)
            OnPotionAdded(potion);
    }

    private void OnDestroy()
    {
        ActivePotions.PotionAdded -= OnPotionAdded;
        ActivePotions.Cleared -= OnActivePotionsCleared;
    }

    private void OnActivePotionsCleared()
    {
        while (_activePotionRenderers.Count > 0)
        {
            var renderer = _activePotionRenderers[0];

            if (renderer.Potion is ExtraHeartPotion)
            {
                OnRendererDestroying(renderer);
                Destroy(renderer);
            }
            else
            {
                throw new System.InvalidOperationException();
            }
        }
    }

    private void OnPotionAdded(Potion potion)
    {
        var activePotionRenderer = Instantiate(_activePotionRendererTemplate, _container);
        activePotionRenderer.Render(potion);
        _activePotionRenderers.Add(activePotionRenderer);
        activePotionRenderer.Destroying += OnRendererDestroying;
    }

    private void OnRendererDestroying(ActivePotionRenderer activePotionRenderer)
    {
        activePotionRenderer.Destroying -= OnRendererDestroying;
        _activePotionRenderers.Remove(activePotionRenderer);
    }
}