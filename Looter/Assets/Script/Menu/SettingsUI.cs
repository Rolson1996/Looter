using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    public GameObject SettingsPanel;

    public GameObject SoundMusicToggleObject;
    private Toggle SoundMusicToggle;

    public GameObject SoundMusicSliderObject;
    private Slider SoundMusicSlider;

    public GameObject SoundEffectsToggleObject;
    private Toggle SoundEffectsToggle;

    public GameObject SoundEffectsSliderObject;
    private Slider SoundEffectsSlider;

    public GameObject VibrateToggleObject;
    private Toggle VibrateToggle;

    public GameObject audioObject;
    private ManageMusic musicManagement;

    void Awake()
    {
        musicManagement = audioObject.GetComponent<ManageMusic>();
    }

    public void OpenPanel()
    {
        SettingsPanel.SetActive(true);
        SetupUI();
    }
    public void ClosePanel()
    {
        SettingsPanel.SetActive(false);
    }

    public void MusicToggleToggled(bool musicActive)
    {
        PlayerPrefs.SetInt("MusicActive", musicActive ? 1 : 0);

        SoundMusicSlider.interactable = musicActive;
        musicManagement.ChangeVolume();
    }

    public void MusicVolumeChanged(Single value)
    {
        PlayerPrefs.SetFloat("MusicVolume", value);
        musicManagement.ChangeVolume();
    }

    public void EffectsToggleToggled(bool effectsActive)
    {
        PlayerPrefs.SetInt("EffectsActive", effectsActive ? 1 : 0);

        SoundEffectsSlider.interactable = effectsActive;
    }

    public void EffectsVolumeChanged(Single value)
    {
        PlayerPrefs.SetFloat("EffectsVolume", value);
    }

    public void VibrateToggleToggled(bool vibrateActive)
    {
        PlayerPrefs.SetInt("VibrateActive", vibrateActive ? 1 : 0);
    }

    private void SetupUI()
    {
        SoundMusicToggle = SoundMusicToggleObject.GetComponent<Toggle>();
        SoundMusicSlider = SoundMusicSliderObject.GetComponent<Slider>();

        SoundEffectsToggle = SoundEffectsToggleObject.GetComponent<Toggle>();
        SoundEffectsSlider = SoundEffectsSliderObject.GetComponent<Slider>();

        VibrateToggle = VibrateToggleObject.GetComponent<Toggle>();

        if (PlayerPrefs.HasKey("MusicActive"))
        {
            SoundMusicToggle.isOn = Convert.ToBoolean(PlayerPrefs.GetInt("MusicActive"));
        }
        else
        {
            PlayerPrefs.SetInt("MusicActive", 1);
        }

        if (PlayerPrefs.HasKey("EffectsActive"))
        {
            SoundEffectsToggle.isOn = Convert.ToBoolean(PlayerPrefs.GetInt("EffectsActive"));
        }
        else
        {
            PlayerPrefs.SetInt("EffectsActive", 1);
        }


        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            SoundMusicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        }
        else
        {
            PlayerPrefs.SetFloat("MusicVolume", 1);
        }

        if (PlayerPrefs.HasKey("EffectsVolume"))
        {
            SoundEffectsSlider.value = PlayerPrefs.GetFloat("EffectsVolume");
        }
        else
        {
            PlayerPrefs.SetFloat("EffectsVolume", 1);
        }

        if(PlayerPrefs.HasKey("VibrateActive"))
        {
            VibrateToggle.isOn = Convert.ToBoolean(PlayerPrefs.GetInt("VibrateActive"));
        }
        else
        {
            PlayerPrefs.SetInt("VibrateActive", 1);
        }
    }


}
