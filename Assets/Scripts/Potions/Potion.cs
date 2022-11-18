using UnityEngine;

public abstract class Potion : MonoBehaviour, IUseable
{
    [SerializeField] private string _lable;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private string _description;
    [SerializeField] private int _cost;

    public string Lable => _lable;
    public Sprite Sprite => _sprite;
    public string Description => _description;
    public int Cost => _cost;

    public abstract void Use(Player player);
}
