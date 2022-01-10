using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuHandler : MonoBehaviour
{
    private void Start()
    {
        // Load Save Data stuff
        SaveManager.SavePath = Application.persistentDataPath + "/Save.bin";
        SaveManager.SetKey = Encoding.UTF8.GetBytes("!CheatYourHighScoreIfYouWantUwU!");
        SaveManager.LoadFromDisk();
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
