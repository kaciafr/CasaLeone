using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingMenu : MonoBehaviour
{
    public Slider masterVol, musicVol, SFXVol;
    
    public AudioMixer mainAudioMixer;
    
    Resolution[] resolutions;
    public TMP_Dropdown dropdownResolution;
    public Toggle Toggle;

    void Start()
    {
        resolutions = Screen.resolutions;
        dropdownResolution.ClearOptions();
        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width
                && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        
        dropdownResolution.AddOptions(options);
        dropdownResolution.value = currentResolutionIndex;
        dropdownResolution.RefreshShownValue();   
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    } 
    
    public void FullScreenMode(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        Debug.Log("FullScreenMode is " + Screen.fullScreen);
    }
    public void ChangeMasterVolume()
    {
        mainAudioMixer.SetFloat("MasterVol", masterVol.value);
    }
    public void ChangeMusicVolume()
    {
        mainAudioMixer.SetFloat("MusicVol", musicVol.value);
    }
    public void ChangeSFXVolume()
    {
        mainAudioMixer.SetFloat("SFXVol", SFXVol.value);
    }
    
}
