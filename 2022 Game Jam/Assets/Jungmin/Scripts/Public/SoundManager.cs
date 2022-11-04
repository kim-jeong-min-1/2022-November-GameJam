using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundEffect
{
    public static string Bow = "Bow";
    public static string CountDown = "CountDown";
    public static string Dakzi = "Dakzi";
    public static string GameOver = "GameOver";
    public static string Hit = "Hit";
    public static string Touch = "Touch";
}

public class SoundManager : Singleton<SoundManager>
{
    private Dictionary<string, AudioClip> sfxSounds = new Dictionary<string, AudioClip>();
    public AudioSource BGM;

    public AudioClip[] Sfxs;
    public AudioClip[] Bgms;

    private void Start()
    {
        foreach (AudioClip auido in Sfxs)
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
        BGM.volume = volum;

        BGM.clip = Bgms[0];
        BGM.Play();

    }
}
