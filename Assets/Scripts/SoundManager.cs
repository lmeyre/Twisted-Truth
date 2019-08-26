using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource musicSource;
    public AudioSource soundSource;

    public AudioClip music;

    public AudioClip monsterDeath;
    public AudioClip monsterAggro;
    public AudioClip monsterAttack;
    public AudioClip monsterStun;

    public AudioClip playerAttack1;
    public AudioClip playerAttack2;
    //public AudioClip playerHit; // player hit = monster attack since cant dodge

    public AudioClip lightOn;
    public AudioClip stairCase;
    public AudioClip lever;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        musicSource.clip = music;
        musicSource.Play();
    }

    // public void StopSound()
    // {
    //     audioSource.Stop();
    // }

    public void PlaySound(AudioClip sound)
    {
        soundSource.PlayOneShot(sound);
    }
}
