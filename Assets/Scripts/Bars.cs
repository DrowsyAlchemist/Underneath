using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Animator))]
public class Bars : MonoBehaviour
{
    [SerializeField] private string _barsLable;

    private const string OpenAnimation = "Open";
    private Animator _animator;
    private bool _isOpen;

    private void OnEnable()
    {
        _animator = GetComponent<Animator>();
        _isOpen = Convert.ToBoolean(PlayerPrefs.GetInt(_barsLable, 0));

        if (_isOpen)
            _animator.Play(OpenAnimation);
    }

    public void Open()
    {
        if (_isOpen == false)
        {
            _animator.Play(OpenAnimation);
            PlayerPrefs.SetInt(_barsLable, 1);
            PlayerPrefs.Save();
        }
    }
}