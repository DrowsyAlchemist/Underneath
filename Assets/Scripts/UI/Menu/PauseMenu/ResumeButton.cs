using UnityEngine;

public class ResumeButton : OpenCloseButton
{
    protected override void Close()
    {
        Time.timeScale = 1;
        base.Close();
    }
}