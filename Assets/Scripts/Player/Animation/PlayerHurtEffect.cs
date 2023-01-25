using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHurtEffect : MonoBehaviour
{
    [SerializeField] private Image _hurtPanel;
    [SerializeField][Range(0, 1)] private float _fadeIntensity = 0.8f;
    [SerializeField] private float _fadeDuration = 0.5f;

    private void Start()
    {
        _hurtPanel.color = Color.clear;
        _hurtPanel.raycastTarget = false;
    }

    public void Play()
    {
        StartCoroutine(Fade());
    }

    private IEnumerator Fade()
    {
        float fadeSpeed = _fadeIntensity / (_fadeDuration / 2);
        float alpha = 0;

        while (_hurtPanel.color.a < _fadeIntensity)
        {
            alpha = Mathf.MoveTowards(alpha, _fadeIntensity, fadeSpeed * Time.deltaTime);
            _hurtPanel.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        while (_hurtPanel.color.a > 0)
        {
            alpha = Mathf.MoveTowards(alpha, 0, fadeSpeed * Time.deltaTime);
            _hurtPanel.color = new Color(0, 0, 0, alpha);
            yield return null;
        }
    }
}