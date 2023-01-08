using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private void Awake()
    {
        AccessPoint.SetEnable(false);
    }

    private void OnDestroy()
    {
        AccessPoint.SetEnable(true);
    }
}
