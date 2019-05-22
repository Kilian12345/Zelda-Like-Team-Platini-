using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderMasterVolume : MonoBehaviour
{
    public float SliderVolume = 0.1f;
    public Slider slider;

    private void Start()
    {
        Slider_Changed(SliderVolume);
        slider.value = SliderVolume;
    }

    public void Slider_Changed(float SliderVolume)
    {
        AudioListener.volume = SliderVolume;
        Debug.Log("test son");
    }
}
