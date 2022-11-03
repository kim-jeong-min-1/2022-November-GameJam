using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundEffect
{
    public static string effectName = ""; 
}

public class SoundManager : Singleton<SoundManager>
{
    private Dictionary<string, AudioClip> sfxSounds = new Dictionary<string, AudioClip>();
    public AudioSource BGM;

    public AudioClip[] Sfxs;
    public AudioClip[] Bgms;

    private void Awake()
    {
        foreach(AudioClip auido in Sfxs)
        {
            sfxSounds.Add(auido.name, auido);
        }
    }

    public void PlaySFX(string soundName, float volume = 1f)
    {
        AudioSource audioSource = new GameObject("sound").AddComponent<AudioSource>();
        audioSource.volume = volume;
        audioSource.playOnAwake = false;
        audioSource.clip = sfxSounds[soundName];

        audioSource.Play();
        Destroy(audioSource.gameObject, audioSource.clip.length);
    }

    public void PlayBGM(float volum = 1f)
    {
        //BGM.volume = volum;
        //if (SceneManager.GetActiveScene().name == "Title")
        //{
        //    BGM.clip = Bgms[0];
        //    BGM.Play();
        //}
    }
}
