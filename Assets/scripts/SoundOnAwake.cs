using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundOnAwake : MonoBehaviour
{
    AudioSource source;
    [SerializeField]
    Sound clip;
    // Start is called before the first frame update
    void Start()
    {
       source = GetComponent<AudioSource>();
       source.clip = clip.getClip();
        source.volume = clip.getVolume();
        source.pitch = Random.Range(clip.getMinPitch(), clip.getMaxPitch());
        source.Play();
    }

}
