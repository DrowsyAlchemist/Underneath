using UnityEngine;

public class NewGameButton : UIButton
{
    protected override void OnButtonClick()
    {
        SceneLoader.LoadScene("Village", Vector3.zero);
    }
}
