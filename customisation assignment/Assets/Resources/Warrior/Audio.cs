using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Audio : MonoBehaviour
{
    public AudioSource clickmaker;

    public void PlaySFX()
    {
        clickmaker.Play();
    }
}
