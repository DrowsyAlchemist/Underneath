using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioOptions : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup _totalAudioMixer;
    [SerializeField] private AudioMixerGroup _musicAudioMixer;
    [SerializeField] private AudioMixerGroup _effectsAudioMixer;
    [SerializeField] private AudioMixerGroup _uiAudioMixer;

    [SerializeField] private Slider _totalVolumeSlider;
    [SerializeField] private Slider _musicVolumeSlider;
    [SerializeField] private Slider _effectsVolumeSlider;
    [SerializeField] private Slider _uiVolumeSlider;

    private const string TotalVolumeName = "TotalVolume";
    private const string MusicVolumeName = "MusicVolume";
    private const string EffectsVolumeName = "EffectsVolume";
    private const string UIVolumeName = "UIVolume";

    private void OnEnable()
    {
        Load();
    }

    private void OnDisable()
    {
        Save();
    }

    public void SetTotalVolume(float volume)
    {
        _totalAudioMixer.audioMixer.SetFloat(TotalVolumeName, Mathf.Lerp(-40, 0, volume));
    }

    public void SetMusicVolume(float volume)
    {
        _musicAudioMixer.audioMixer.SetFloat(MusicVolumeName, Mathf.Lerp(-40, 0, volume));
    }

    public void SetEffectsVolume(float volume)
    {
        _effectsAudioMixer.audioMixer.SetFloat(EffectsVolumeName, Mathf.Lerp(-40, 0, volume));
    }

    public void SetUIVolume(float volume)
    {
        _uiAudioMixer.audioMixer.SetFloat(UIVolumeName, Mathf.Lerp(-40, 0, volume));
    }

    public void Save()
    {
        PlayerPrefs.SetFloat(TotalVolumeName, _totalVolumeSlider.value);
        PlayerPrefs.SetFloat(MusicVolumeName, _musicVolumeSlider.value);
        PlayerPrefs.SetFloat(EffectsVolumeName, _effectsVolumeSlider.value);
        PlayerPrefs.SetFloat(UIVolumeName, _uiVolumeSlider.value);
    }

    public void Load()
    {
        LoadParameter(TotalVolumeName, _totalAudioMixer, _totalVolumeSlider);
        LoadParameter(MusicVolumeName, _musicAudioMixer, _musicVolumeSlider);
        LoadParameter(EffectsVolumeName, _effectsAudioMixer, _effectsVolumeSlider);
        LoadParameter(UIVolumeName, _uiAudioMixer, _uiVolumeSlider);
    }

    private void LoadParameter(string volumeName, AudioMixerGroup mixer, Slider slider)
    {
        float volume = PlayerPrefs.GetFloat(volumeName, 1);
        mixer.audioMixer.SetFloat(UIVolumeName, Mathf.Lerp(-40, 0, volume));
        slider.value = volume;
    }
}