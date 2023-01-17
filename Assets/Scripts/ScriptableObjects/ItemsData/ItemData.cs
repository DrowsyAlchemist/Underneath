using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "ScriptableObjects/Item", order = 51)]
public class ItemData : ScriptableObject
{
    [SerializeField] private string _lable;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private string _description;
    [SerializeField] private int _cost;
    [SerializeField] private string _saveFileName;

    public string Lable => _lable;
    public Sprite Sprite => _sprite;
    public string Description => _description;
    public int Cost => _cost;
    public string SaveFileName => _saveFileName; // Prefab file in Resources folder must be named the same
}
