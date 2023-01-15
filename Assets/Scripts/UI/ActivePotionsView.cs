using UnityEngine;

public class ActivePotionsView : MonoBehaviour
{
    [SerializeField] private RectTransform _activePotionsContainer;
    [SerializeField] private ActivePotionRenderer _activePotionRenderer;

    public void SetPotion(Potion potion)
    {
        var activePotionRenderer = Instantiate(_activePotionRenderer, _activePotionsContainer);
        activePotionRenderer.Render(potion);
    }
}
