using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinsRenderer : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private TMP_Text _coinsCount;

    private void OnEnable()
    {
        _player.MoneyChanged += OnMoneyChanged;
    }

    private void OnDisable()
    {
        _player.MoneyChanged -= OnMoneyChanged;
    }

    private void OnMoneyChanged(int money)
    {
        _coinsCount.text = money.ToString();
    }
}
