using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager AudioInstance;

    public Sound[] MusicSound, SfxSound;
    public AudioSource MusicSource;
    public AudioSource SfxSource;

    private void Awake()
    {
        if (AudioInstance == null)
        {
            AudioInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        { 
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayMusic("Music");
    }

    public void PlayMusic(string name) {
        Sound music = Array.Find(MusicSound, x => x.name == name);

        if (music == null)
        {
            Debug.Log("Sound not found");
        }
        else { 
            MusicSource.clip = music.clip;
            MusicSource.Play();
        }
    }

    public void MusicVolume(float volume) { 
        MusicSource.volume = volume;
    }

    public void PlaySfx(string name)
    {
        Sound sfx = Array.Find(SfxSound, x => x.name == name);

        if (sfx == null)
        {
            Debug.Log("Sound not found");
        }
        else
        {
            SfxSource.PlayOneShot(sfx.clip);
        }
    }

    public void SfxVolume(float volume)
    {
        SfxSource.volume = volume;
    }
}   
