using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsHandler : MonoBehaviour
{
    private GameObject MainUI;
    private GameObject SettingsUI;
    private Toggle FastSpeed;
    private Toggle DiscordPresence;
    private void Awake()
    {
        MainUI = transform.Find("MainUI").gameObject;
        SettingsUI = transform.Find("SettingsUI").gameObject;
        FastSpeed = SettingsUI.transform.Find("FastToggle").GetComponent<Toggle>();
        DiscordPresence = SettingsUI.transform.Find("DiscordPresence").GetComponent<Toggle>();
    }
    void Start()
    {
#if !PLATFORM_STANDALONE
        // Well, it a different platform so we'll have to disable discord presence settings
        DiscordPresence.interactable = false;
        DiscordPresence.isOn = false;
#endif
    }
    public void showSettings()
    {
        // Init Current Settings
        FastSpeed.isOn = SaveManager.Data.FastSpeed;
#if PLATFORM_STANDALONE
        DiscordPresence.isOn = SaveManager.Data.DiscordPresence;
#endif
        // Load the UI
        MainUI.SetActive(false);
        SettingsUI.SetActive(true);
    }
    public void hideSettings()
    {
        SettingsUI.SetActive(false);
        MainUI.SetActive(true);
    }
    public void saveSettings()
    {
        SaveData Data = SaveManager.Data;
        Data.FastSpeed = FastSpeed.isOn;
        Data.DiscordPresence = DiscordPresence.isOn;
        SaveManager.SaveToDisk();
        hideSettings();
    }
}
