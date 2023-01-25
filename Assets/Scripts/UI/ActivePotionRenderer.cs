using UnityEngine;
using UnityEngine.UI;

public class ActivePotionRenderer : MonoBehaviour
{
    [SerializeField] private Image _image;

    private float _elapsedTime;
    private float _delay;

    public Potion Potion { get; private set; }

    private void Awake()
    {
        enabled = false;
    }

    public void Render(Potion potion)
    {
        Potion = potion;
        _image.sprite = potion.Data.Sprite;
        potion.transform.SetParent(transform);
        _delay = potion.Duration;
        enabled = true;
    }

    private void Update()
    {
        _elapsedTime += Time.deltaTime;

        if(_elapsedTime > _delay)
            Destroy(gameObject);
    }
}