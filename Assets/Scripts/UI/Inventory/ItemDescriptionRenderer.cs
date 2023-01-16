using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemDescriptionRenderer : MonoBehaviour
{
    [SerializeField] private TMP_Text _lable;
    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _description;

    private void OnEnable()
    {
        Clear();
    }

    public void Render(Item item)
    {
        _lable.text = item.Data.Lable;
        _image.sprite = item.Data.Sprite;
        _image.color = Color.white;
        _description.text = item.Data.Description;
    }

    public void Clear()
    {
        _lable.text = "";
        _image.sprite = null;
        _image.color = Color.clear;
        _description.text = "";
    }
}