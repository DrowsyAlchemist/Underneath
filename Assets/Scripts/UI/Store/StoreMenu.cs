using UnityEngine;

public class StoreMenu : MonoBehaviour
{
    [SerializeField] private WareRenderer _wareRenderer;
    [SerializeField] private RectTransform _waresContainer;
    [SerializeField] private GameObject _notEnoughMoneyPanel;

    private Inventory _playerInventory;
    private Wallet _playerWallet;

    private void Start()
    {
        _playerInventory = AccessPoint.Player.Inventory;
        _playerWallet = AccessPoint.Player.Wallet;
    }

    public void AddWare(Item item)
    {
        var wareRenderer = Instantiate(_wareRenderer, _waresContainer);
        wareRenderer.Render(item);
        wareRenderer.ButtonClicked += OnBuyButtonClick;
    }

    private void OnBuyButtonClick(WareRenderer wareRenderer)
    {
        if (_playerWallet.Money >= wareRenderer.Cost)
        {
            _playerWallet.GiveMoney(wareRenderer.Cost);
            _playerInventory.AddItem(wareRenderer.Item);
        }
        else
        {
            MessageCreator.ShowMessage("Not enough money :(", (RectTransform)transform, MessageType.Message);
        }
    }
}
