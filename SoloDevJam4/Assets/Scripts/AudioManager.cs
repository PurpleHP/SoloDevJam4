using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [SerializeField] Slider masterSlider;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;
    [SerializeField] AudioMixer audioMixer;
    private const string MasterVolumeParam = "MasterVolume";
    private const string MusicVolumeParam = "MusicVolume";
    private const string SFXVolumeParam = "SFXVolume";
    

    private void Start()
    {
        masterSlider.onValueChanged.AddListener(SetMasterVolume);
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);

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

    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat(MasterVolumeParam, Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat(MasterVolumeParam, volume);
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

    public float GetMasterVolume()
    {
        audioMixer.GetFloat(MasterVolumeParam, out float value);
        return Mathf.Pow(10, value / 20);
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
      
        float masterVolume = PlayerPrefs.GetFloat(MasterVolumeParam, 0.75f);
        float musicVolume = PlayerPrefs.GetFloat(MusicVolumeParam, 0.75f);
        float sfxVolume = PlayerPrefs.GetFloat(SFXVolumeParam, 0.75f);

        SetMasterVolume(masterVolume);
        SetMusicVolume(musicVolume);
        SetSFXVolume(sfxVolume);
        
        masterSlider.value = GetMasterVolume();
        musicSlider.value = GetMusicVolume();
        sfxSlider.value = GetSFXVolume();
    }

   
}
