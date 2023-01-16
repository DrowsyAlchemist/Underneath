using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ActivePotionRenderer : MonoBehaviour
{
    [SerializeField] private Image _image;

    public void Render(Potion potion)
    {
        _image.sprite = potion.Data.Sprite;
        potion.transform.SetParent(transform);
        StartCoroutine(DestroyWithDelay(potion.Duration));
    }

    public IEnumerator DestroyWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
