using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Audio : MonoBehaviour
{
    public AudioSource clickmaker;
    public AudioSource musicSource;
    public AudioClip oof;
    public AudioClip click;
    public AudioClip Boop;
    public AudioClip deathMusic;
    public AudioClip music;
    public AudioClip levelupnoise;
    public CanvasGroup death;
    public static bool dead;
    public static bool pain;
    public static bool boop;
    public static bool levelup;


    private void Start()
    {
        musicSource.clip = music;
        musicSource.Play();
    }


    public void PlaySFX()
    {
        clickmaker.clip = click;
        clickmaker.Play();
    }

    //unecessarily expensive music trigger.
    private void Update()
    {

        if (pain)
        {
            clickmaker.clip = oof;
            clickmaker.Play();
            pain = false;
        }
        if (boop)
        {
            clickmaker.clip = Boop;
            clickmaker.Play();
            boop = false;
        }

        if (levelup)
        {
            clickmaker.clip = levelupnoise;
            clickmaker.Play();
            levelup = false;
        }

        if (dead)
        {
            Die();
        }
        else
        {
            return;
        }



    }


    public void Die()
    {
        if(musicSource.clip == deathMusic)
        {
            return;
        }
        musicSource.Stop();
        musicSource.clip = deathMusic;
        musicSource.Play();
    }

    public void ResPawnMusic()
    {
        dead = false;
        musicSource.Stop();
        musicSource.clip = music;
        musicSource.Play();
    }

}

