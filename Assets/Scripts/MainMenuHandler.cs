using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using Discord;

public class MainMenuHandler : MonoBehaviour
{
    private void Start()
    {
        // Load Save Data stuff
        SaveManager.SavePath = Application.persistentDataPath + "/Save.bin";
        SaveManager.SetKey = Encoding.UTF8.GetBytes("!CheatYourHighScoreIfYouWantUwU!");
        SaveManager.LoadFromDisk();
        // 30fps for android
#if PLATFORM_STANDALONE
        Activity MenuAct = new Activity();
        MenuAct.Details = "Main Menu...";
        MenuAct.Assets.LargeImage = "logo";
        MenuAct.Assets.LargeText = "By Fur Tech Corp.";
        DiscordManager.SetPresence(MenuAct);
#endif
    }
    private void Update()
    {
#if PLATFORM_STANDALONE
        if(DiscordManager.CallerReady)
            DiscordManager.UpdateCaller();
#endif
    }
    public void Startbtn_Handler()
    {
        SceneManager.LoadScene("MainGame", LoadSceneMode.Single);
    }
    public void Quitbtn_Handler()
    {
        Application.Quit();
    }
}
