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

    public void AddWare(UseableItem potion)
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

            if (wareRenderer.Item.TryGetComponent(out PermanentEffectItem _))
                Destroy(wareRenderer.gameObject);
        }   
        else
        {
            _notEnoughMoneyPanel.SetActive(true);
        }
    }
}
