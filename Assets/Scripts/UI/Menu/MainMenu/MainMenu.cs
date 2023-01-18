using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button _newGameButton;
    [SerializeField] private Button _cuntinueGameButton;
    [SerializeField] private Vector3 _newGamePosition = Vector3.zero;
    [SerializeField] private AccessPoint _accessPoint;

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
        if (AccessPoint.HasInstance == false)
            Instantiate(_accessPoint);

        SaveLoadManager.RemoveAllSaves();
        var player = AccessPoint.Player;
        player.ResetPlayer();
        SceneLoader.LoadScene("Village", _newGamePosition);
    }

    private void OnContinueButtonClick()
    {
        if (AccessPoint.HasInstance == false)
            Instantiate(_accessPoint);

        var player = AccessPoint.Player;
        string sceneName = player.GetSavedSceneName();
        sceneName ??= "Village";
        player.ResetPlayer();
        SceneLoader.LoadScene(sceneName, player.GetSavedPosition());
    }
}
