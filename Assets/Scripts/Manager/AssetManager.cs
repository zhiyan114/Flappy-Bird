using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetManager : MonoBehaviour
{
    public static AssetManager instance;
    private void Awake()
    {
        instance = this;
    }
    public Transform HeadPipeSprite;
    public Transform BodyPipeSprite;
}
