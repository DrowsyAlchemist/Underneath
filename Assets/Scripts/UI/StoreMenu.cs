using UnityEngine;

public class StoreMenu : MonoBehaviour
{
    [SerializeField] private WareRenderer _wareRenderer;
    [SerializeField] private RectTransform _waresContainer;
    [SerializeField] private GameObject _notEnoughMoneyPanel;

    private Player _player;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void Init(Player player)
    {
        _player = player;
    }

    public void AddWare(Potion potion)
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
            _player.Inventory.AddPotion(wareRenderer.Potion);
        }   
        else
        {
            _notEnoughMoneyPanel.SetActive(true);
        }
    }
}
