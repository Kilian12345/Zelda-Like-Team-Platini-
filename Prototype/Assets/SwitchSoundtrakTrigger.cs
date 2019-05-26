using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SwitchSoundtrakTrigger : MonoBehaviour
{
    public AudioClip newTrack;

    [SerializeField] FadingAudioSource FAS;

    
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && newTrack != null)
        {
            FAS.Fade(newTrack, 1.0f, true);
        }
    }
}
