using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProtoBuf;


public class SoundHandler : MonoBehaviour
{

    private AudioSource SoundService;
    private static SoundHandler instance;
    private void Awake()
    {
        instance = this;
        SoundService = GetComponent<AudioSource>();
    }
    public static void PlaySound(SoundOption SoundOpt)
    {

        switch(SoundOpt)
        {
            case SoundOption.Die:
                instance.SoundService.PlayOneShot(GetSound("die"));
                break;
            case SoundOption.Hit:
                instance.SoundService.PlayOneShot(GetSound("hit"));
                break;
            case SoundOption.Point:
                instance.SoundService.PlayOneShot(GetSound("point"));
                break;
            case SoundOption.Swoosh:
                instance.SoundService.PlayOneShot(GetSound("woosh"));
                break;
            case SoundOption.Wing:
                instance.SoundService.PlayOneShot(GetSound("wing"));
                break;
        }
    }
    private static AudioClip GetSound(string name)
    {
        return Resources.Load<AudioClip>("Audio/" + name);
    }
}
