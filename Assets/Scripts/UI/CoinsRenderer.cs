using TMPro;
using UnityEngine;

public class CoinsRenderer : MonoBehaviour
{
    [SerializeField] private TMP_Text _coinsCountText;
    [SerializeField] private Player _player;

    private Wallet _playerWallet;

    private void Start()
    {
        _playerWallet = _player.Wallet;
        _playerWallet.MoneyChanged += OnMoneyChanged;
        OnMoneyChanged(_playerWallet.Money);
    }

    private void OnDestroy()
    {
        _playerWallet.MoneyChanged -= OnMoneyChanged;
    }

    private void OnMoneyChanged(int money)
    {
        _coinsCountText.text = money.ToString();
    }
}