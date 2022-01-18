using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Discord;
using System;

public static class DiscordManager
{
    // Only use discord presence in production otherwise it bad :/
#if PLATFORM_STANDALONE && !UNITY_EDITOR
    private static Discord.Discord DiscordCaller = SaveManager.Data.DiscordPresence ? new Discord.Discord(932403884788432927, (UInt64)CreateFlags.NoRequireDiscord) : null;
#else
    private static Discord.Discord DiscordCaller = null;
#endif

    public static bool CallerReady
    {
        get => DiscordCaller != null;
    }
    public static void SetPresence(Activity PresenceAct, ActivityManager.UpdateActivityHandler cb = null)
    {
        if (!CallerReady) return;
        if (cb == null) cb = (Result _) => { };
        DiscordCaller.GetActivityManager().UpdateActivity(PresenceAct, cb);
    }
    public static void SetPresence(GameState PlrState, int HighScore, int NewScore, ActivityManager.UpdateActivityHandler cb = null)
    {
        if (!CallerReady) return;
        if (cb == null) cb = (Result _) => { };
        Activity MainGameAct = new Activity();
        MainGameAct.Details = "Currently in-game";
        switch (PlrState)
        {
            case GameState.WaitToStart:
                MainGameAct.State = "Waiting to start...";
                break;
            case GameState.Playing:
                MainGameAct.State = "Playing...";
                break;
            case GameState.Dead:
                MainGameAct.State = "Dead...";
                break;
        }
        if(HighScore > 0 && NewScore > 0 )
        {
            MainGameAct.Party.Id = "PartyID_" + UnityEngine.Random.Range(1000, 100000);
            PartySize PlrScorePresence = new PartySize();
            PlrScorePresence.CurrentSize = NewScore;
            PlrScorePresence.MaxSize = HighScore;
            MainGameAct.Party.Size = PlrScorePresence;
        }
        MainGameAct.Assets.LargeImage = "logo";
        MainGameAct.Assets.LargeText = "By Fur Tech Corp.";
        DiscordCaller.GetActivityManager().UpdateActivity(MainGameAct, cb);
    }
    public static void UpdateCaller()
    {
        if (!CallerReady) return;
        DiscordCaller.RunCallbacks();
    }
}
