using UnityEngine;

public class UISounds : MonoBehaviour
{
    [SerializeField] private AudioSource _potionDrinkSound;
    [SerializeField] private AudioSource _equipSound;

    private static UISounds _instance;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(gameObject);
    }

    public static void PlayEquip()
    {
        _instance._equipSound.Play();
    }

    public static void PlayDrinkPotion()
    {
        _instance._potionDrinkSound.Play();
    }
}
