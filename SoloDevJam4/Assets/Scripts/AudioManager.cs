using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioMixer audioMixer;
    private const string MusicVolumeParam = "MusicVolume";
    private const string SFXVolumeParam = "SFXVolume";
    public Slider musicSlider;
    public Slider sfxSlider;

    void Start()
    {
        musicSlider.value = GetMusicVolume();
        sfxSlider.value = GetSFXVolume();
    }
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            LoadVolumeSettings();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat(MusicVolumeParam, Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat(MusicVolumeParam, volume);
    }

    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat(SFXVolumeParam, Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat(SFXVolumeParam, volume);
    }

    public float GetMusicVolume()
    {
        audioMixer.GetFloat(MusicVolumeParam, out float value);
        return Mathf.Pow(10, value / 20);
    }

    public float GetSFXVolume()
    {
        audioMixer.GetFloat(SFXVolumeParam, out float value);
        return Mathf.Pow(10, value / 20);
    }

    private void LoadVolumeSettings()
    {
        float musicVolume = PlayerPrefs.GetFloat(SFXVolumeParam, 1f);
        float sfxVolume = PlayerPrefs.GetFloat(SFXVolumeParam, 1f);

        SetMusicVolume(musicVolume);
        SetSFXVolume(sfxVolume);
    }
}