using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Sound[] sounds;

    void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source =  gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.outputAudioMixerGroup  = s.output;
            s.source.playOnAwake = s.playOnAwake;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }

        GameObject[] audioManagers = GameObject.FindGameObjectsWithTag("AudioManager");

        if (audioManagers.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        Play("Bg_Music");
    }

    public void Play(string name)
    {
       Sound s = Array.Find(sounds, sounds => sounds.name == name); 
       if (s == null)
       {
           return;
       }
       s.source.Play(); 
    }
}
