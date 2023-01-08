using UnityEngine;

public class MainMenuButton : OpenCloseButton
{
    [SerializeField] private string _mainMenuSceneName;

    protected override void Close()
    {
        base.Close();
        Time.timeScale = 1;
        SceneLoader.LoadScene(_mainMenuSceneName, Vector3.zero);
    }
}
