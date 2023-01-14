using UnityEngine;
using UnityEngine.UI;

public class ActivePotionView : MonoBehaviour
{
    public static ActivePotionView Create(Potion potion, Transform container)
    {
        ActivePotionView potionView = new GameObject().AddComponent<ActivePotionView>();
        potionView.gameObject.AddComponent<Image>().sprite = potion.Data.Sprite;
        potionView.transform.SetParent(container);
        potionView.GetComponent<RectTransform>().localScale = Vector3.one;
        potion.AffectingFinished += potionView.OnPotionStopAffecting;
        return potionView;
    }

    private void OnPotionStopAffecting(Potion potion)
    {
        potion.AffectingFinished -= OnPotionStopAffecting;
        Destroy(gameObject);
    }
}
