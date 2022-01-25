using ProtoBuf;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ProtoContract]
public enum GameState
{
    [ProtoEnum]
    WaitToStart,
    [ProtoEnum]
    Playing,
    [ProtoEnum]
    Dead,
}
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

/*
public static class EnumCollection
{
}
*/