using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OnOffMusicButton : UIButton
{
    [SerializeField] private Image _image;
    [SerializeField] private Sprite _musicOffSprite;
    [SerializeField] private AudioMixerGroup _musicAudioMixer;

    private const string MusicVolumeName = "MusicVolume";
    private bool _isMusicOn;
    private Sprite _musicOnSprite;

    private void Awake()
    {
        _isMusicOn = true;
        _musicOnSprite = _image.sprite;
    }

    protected override void OnButtonClick()
    {
        _isMusicOn = (_isMusicOn == false);
        _musicAudioMixer.audioMixer.SetFloat(MusicVolumeName, _isMusicOn ? 0 : -80);
        _image.sprite = _isMusicOn ? _musicOnSprite : _musicOffSprite;
    }
}