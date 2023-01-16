using UnityEngine;

public class StoreMenu : MonoBehaviour
{
    [SerializeField] private WareRenderer _wareRenderer;
    [SerializeField] private RectTransform _waresContainer;
    [SerializeField] private GameObject _notEnoughMoneyPanel;

    private Player _player;

    private void Start()
    {
        _player = AccessPoint.Player;
    }

    public void AddWare(Item potion)
    {
        var wareRenderer = Instantiate(_wareRenderer, _waresContainer);
        wareRenderer.Render(potion);
        wareRenderer.ButtonClicked += OnBuyButtonClick;
    }

    private void OnBuyButtonClick(WareRenderer wareRenderer)
    {
        if (_player.Money >= wareRenderer.Cost)
        {
            _player.GiveMoney(wareRenderer.Cost);
            _player.Inventory.AddItem(wareRenderer.Item);
        }   
        else
        {
            MessageCreator.ShowMessage("Not enough money :(", (RectTransform)transform, MessageType.Message);
        }
    }
}
