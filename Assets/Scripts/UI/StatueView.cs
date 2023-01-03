using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class StatueView : MonoBehaviour
{
    [SerializeField] private TMP_Text _lable;
    [SerializeField] private Button _button;

    private Statue _statue;

    public event UnityAction<Statue> ButtonClicked;

    public void Render(Statue statue)
    {
        _statue = statue;
        _lable.text = statue.SceneName;
    }

    private void Start()
    {
        _button.onClick.AddListener(OnButtonClick);
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        ButtonClicked?.Invoke(_statue);
    }
}
