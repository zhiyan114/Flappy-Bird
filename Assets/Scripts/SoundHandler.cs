using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProtoBuf;
[ProtoContract]
public enum SoundOption
{
    [ProtoEnum]
    Die,
    [ProtoEnum]
    Hit,
    [ProtoEnum]
    Point,
    [ProtoEnum]
    Swoosh,
    [ProtoEnum]
    Wing,
}

public class SoundHandler : MonoBehaviour
{
    [SerializeField]
    private AudioClip Die;
    [SerializeField]
    private AudioClip Hit;
    [SerializeField]
    private AudioClip Point;
    [SerializeField]
    private AudioClip Swoosh;
    [SerializeField]
    private AudioClip Wing;

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
                instance.SoundService.PlayOneShot(instance.Die);
                break;
            case SoundOption.Hit:
                instance.SoundService.PlayOneShot(instance.Hit);
                break;
            case SoundOption.Point:
                instance.SoundService.PlayOneShot(instance.Point);
                break;
            case SoundOption.Swoosh:
                instance.SoundService.PlayOneShot(instance.Swoosh);
                break;
            case SoundOption.Wing:
                instance.SoundService.PlayOneShot(instance.Wing);
                break;
        }
    }
}
