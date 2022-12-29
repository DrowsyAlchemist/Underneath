using TMPro;
using UnityEngine;

public class CoinsRenderer : MonoBehaviour
{
    [SerializeField] private TMP_Text _coinsCount;

    private Player _player;

    private void Start()
    {
        _player = AccessPoint.Player;
        _player.MoneyChanged += OnMoneyChanged;
        OnMoneyChanged(_player.Money);
    }

    private void OnDestroy()
    {
        _player.MoneyChanged -= OnMoneyChanged;
    }

    private void OnMoneyChanged(int money)
    {
        _coinsCount.text = money.ToString();
    }
}
