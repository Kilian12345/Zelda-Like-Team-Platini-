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
        //Slider_Changed(SliderVolume);  
    }

    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Slider"))
        {
            slider = GameObject.FindGameObjectWithTag("Slider").GetComponent<Slider>();
        }
        if (slider)
        {
            if (PlayerPrefs.GetFloat("Volume") != 0)
            {
                SliderVolume = PlayerPrefs.GetFloat("Volume");
                Debug.Log("test son");
            }
            slider.value = SliderVolume;
        }
        
    }

    public void Slider_Changed(float SliderVolume)
    {
        PlayerPrefs.SetFloat("Volume", SliderVolume);
        AudioListener.volume = SliderVolume;
        //Debug.Log("test son");
    }
}
