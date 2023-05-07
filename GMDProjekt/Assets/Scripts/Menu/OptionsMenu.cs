using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{

    public Slider VolumeSlider;

    private void Awake()
    {
    }

    public void SetNewMasterVolume()
    {
        var masterVolume = VolumeSlider.value;
        PlayerPrefs.SetFloat("MasterVolume",masterVolume);
    }
}
