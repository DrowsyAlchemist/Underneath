using TMPro;
using UnityEngine;

public class CoinsRenderer : MonoBehaviour
{
    [SerializeField] private TMP_Text _coinsCount;

    private Wallet _playerWallet;

    private void Start()
    {
        _playerWallet = AccessPoint.Player.Wallet;
        _playerWallet.MoneyChanged += OnMoneyChanged;
        OnMoneyChanged(_playerWallet.Money);
    }

    private void OnDestroy()
    {
        _playerWallet.MoneyChanged -= OnMoneyChanged;
    }

    private void OnMoneyChanged(int money)
    {
        _coinsCount.text = money.ToString();
    }
}
