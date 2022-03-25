using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

public class OptionsScript : MonoBehaviour
{
    public GameObject keyBoardConfig;
    public GameObject Audio;
    public Toggle fullscreenToggle, vsyncToggle;
    public List<ResItem> resolutions = new List<ResItem>();
    private int selectedResolution;
    public TMP_Text resolutionLabel;

    [Header("Audio")]
    public AudioMixer theMixer;
    public TMP_Text masterLabel, musicLabel, sfxLabel;
    public Slider masterSlider, musicSlider, sfxSlider;

    void Start()
    {
        fullscreenToggle.isOn = Screen.fullScreen;

        if (QualitySettings.vSyncCount == 0)
        {
            vsyncToggle.isOn = false;
        }
        else
            vsyncToggle.isOn = true;

        bool foundNativeRes = false;
        for (int i = 0; i < resolutions.Count; i++)
        {
            if (Screen.width == resolutions[i].horizontal && Screen.height == resolutions[i].vertical)
            {
                foundNativeRes = true;
                selectedResolution = i;
                updateResLabel();
            }
        }

        if(!foundNativeRes)
        {
            ResItem weirdRes = new ResItem();
            weirdRes.horizontal = Screen.width;
            weirdRes.vertical = Screen.height;

            resolutions.Add(weirdRes);
            selectedResolution = resolutions.Count - 1;
            updateResLabel();
        }

        float volume = 0;
        theMixer.GetFloat("MasterVolume", out volume);
        masterSlider.value = volume;
        theMixer.GetFloat("MusicVolume", out volume);
        musicSlider.value = volume;
        theMixer.GetFloat("SFXVolume", out volume);
        sfxSlider.value = volume;

        masterLabel.text = Mathf.RoundToInt(masterSlider.value + 80).ToString();
        musicLabel.text = Mathf.RoundToInt(musicSlider.value + 80).ToString();
        sfxLabel.text = Mathf.RoundToInt(sfxSlider.value + 80).ToString();
    }

    public void resLeft()
    {
        if (selectedResolution <= 0)
        {
            selectedResolution = resolutions.Count - 1;
        }
        else
            selectedResolution--;

        updateResLabel();
    }

    public void resRight()
    {
        if (selectedResolution >= resolutions.Count - 1)
        {
            selectedResolution = 0;
        }
        else
            selectedResolution++;

        updateResLabel();
    }

    public void updateResLabel()
    {
        resolutionLabel.text = resolutions[selectedResolution].horizontal.ToString() + " x " + resolutions[selectedResolution].vertical.ToString();
    }

    public void applyGraphics()
    {

        if (vsyncToggle.isOn)
            QualitySettings.vSyncCount = 1;
        else
            QualitySettings.vSyncCount = 0;

        Screen.SetResolution(resolutions[selectedResolution].horizontal, resolutions[selectedResolution].vertical, fullscreenToggle.isOn);
    }

    public void setMasterVolume()
    {
        masterLabel.text = Mathf.RoundToInt(masterSlider.value + 80).ToString();
        theMixer.SetFloat("MasterVolume", masterSlider.value);

        PlayerPrefs.SetFloat("MasterVolume", masterSlider.value); //basic float value saving using playerprefs which is a type unity provides
    }

    public void setMusicVolume()
    {
        musicLabel.text = Mathf.RoundToInt(musicSlider.value + 80).ToString();
        theMixer.SetFloat("MusicVolume", musicSlider.value);

        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value); //basic float value saving using playerprefs which is a type unity provides
    }

    public void setSFXVolume()
    {
        sfxLabel.text = Mathf.RoundToInt(sfxSlider.value + 80).ToString();
        theMixer.SetFloat("SFXVolume", sfxSlider.value);

        PlayerPrefs.SetFloat("SFXVolume", sfxSlider.value); //basic float value saving using playerprefs which is a type unity provides
    }

    public void openKeyBoardConfig()
    {
        keyBoardConfig.SetActive(true);
    }

    public void closeKeyBoardConfig()
    {
        keyBoardConfig.SetActive(false);
    }

    public void openAudioOptions()
    {
        Audio.SetActive(true);
    }

    public void closeAudioOptions()
    {
        Audio.SetActive(false);
    }
}

[System.Serializable]
public class ResItem
{
    public int horizontal, vertical;
}