using System.Collections.Generic;
using UnityEngine;

public class LightControl : MonoBehaviour
{
    [SerializeField] private List<SpriteRenderer> _renderers;
    [SerializeField][Range(0, 1)] private float _fadeIntensity;

    private Color _fadeColor;

    private void Start()
    {
        float color = 1 - _fadeIntensity;
        _fadeColor = new Color(color, color, color);
    }

    public void Lit()
    {
        foreach (var renderer in _renderers)
            renderer.color = Color.white;
    }

    public void Unlit()
    {
        foreach (var renderer in _renderers)
            renderer.color = _fadeColor;
    }
}
