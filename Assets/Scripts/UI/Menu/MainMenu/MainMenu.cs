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
        var player = AccessPoint.Player;
        player.ResetPlayer();
        SceneLoader.LoadScene("Village", player.GetSavedPosition());
    }

    private void OnContinueButtonClick()
    {
        var player = AccessPoint.Player;
        string sceneName = player.GetSavedSceneName();
        sceneName ??= "Village";
        SceneLoader.LoadScene(sceneName, player.GetSavedPosition());
    }
}
