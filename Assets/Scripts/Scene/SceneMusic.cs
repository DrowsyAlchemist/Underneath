using UnityEngine;

public class SceneMusic : MonoBehaviour
{
    [SerializeField] private AudioSource _mainMenuMusic;
    [SerializeField] private AudioSource _gameMusic;

    public void PlayMainMenuMusic()
    {
        _gameMusic.Stop();
        _mainMenuMusic.Play();
    }

    public void PlayGameMusic()
    {
        _mainMenuMusic.Stop();
        _gameMusic.Play();
    }
}
