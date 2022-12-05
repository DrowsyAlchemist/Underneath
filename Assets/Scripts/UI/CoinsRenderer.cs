using TMPro;
using UnityEngine;

public class CoinsRenderer : MonoBehaviour
{
    [SerializeField] private AccessPoint _game;
    [SerializeField] private TMP_Text _coinsCount;

    private Player _player;

    private void Start()
    {
        _player = _game.Player;
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
