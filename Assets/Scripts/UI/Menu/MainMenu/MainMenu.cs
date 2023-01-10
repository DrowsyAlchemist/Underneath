using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button _newGameButton;
    [SerializeField] private Button _cuntinueGameButton;

    private void Start()
    {
        _newGameButton.onClick.AddListener(OnNewGameButtonClick);
        _cuntinueGameButton.onClick.AddListener(OnContinueButtonClick);
    }

    private void OnDestroy()
    {
        _newGameButton.onClick.RemoveListener(OnNewGameButtonClick);
        _cuntinueGameButton.onClick.RemoveListener(OnContinueButtonClick);
    }

    private void OnNewGameButtonClick()
    {
        SaveLoadManager.RemoveAllSaves();
        SceneLoader.LoadScene("Village", Vector3.zero);
    }

    private void OnContinueButtonClick()
    {
        SceneLoader.LoadScene("Village", Vector3.zero);
    }
}
