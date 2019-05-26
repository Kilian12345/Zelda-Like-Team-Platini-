using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SwitchSoundtrak : MonoBehaviour
{
    public AudioSource SM;

    public void ChangeSM(AudioClip music)
    {
        if (SM.clip.name != music.name)
        {
            SM.Stop();
            SM.clip = music;
            SM.Play();
        }
    }
}
