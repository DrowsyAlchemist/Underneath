using UnityEngine;

public class ExitButton : UIButton
{
    protected override void OnButtonClick()
    {
        Application.Quit();
    }
}