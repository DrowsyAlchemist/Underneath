using TMPro;
using UnityEngine;

public class CoinsRenderer : MonoBehaviour
{
    [SerializeField] private Game _game;
    [SerializeField] private TMP_Text _coinsCount;

    private Player _player;

    private void Awake()
    {
        _player = _game.Player;
    }

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
