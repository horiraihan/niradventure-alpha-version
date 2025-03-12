using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    public AudioSource musicSource;
    public Slider volumeSlider;

    private void Start()
    {
        // Set the Slider value to its maximum
        volumeSlider.value = volumeSlider.maxValue;

        // Set the initial volume based on the Slider value
        musicSource.volume = volumeSlider.value;
    }

    public void UpdateVolume()
    {
        // Update the volume based on the Slider value
        musicSource.volume = volumeSlider.value;
    }
}