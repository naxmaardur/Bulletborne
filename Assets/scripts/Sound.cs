using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
[System.Serializable]
public class Sound
{
    [SerializeField]
    private AudioClip clip;
    
    [SerializeField]
    [Range(0,1)]
    private float volume = 1;
    
    [SerializeField]
    [Range(0.1f,3)]
    private float MinPitch = 0.9f;
    [SerializeField]
    [Range(0.1f, 3)]
    private float MaxPitch = 1.1f;

    public AudioClip getClip()
    {
        return clip;
    }

    public float getVolume()
    {
        return volume;
    }

    public float getMinPitch()
    {
        return MinPitch;
    }

    public float getMaxPitch()
    {
        return MaxPitch;
    }

    public float getRandomPitch()
    {
        return Random.Range(MinPitch, MaxPitch);
    }
}
