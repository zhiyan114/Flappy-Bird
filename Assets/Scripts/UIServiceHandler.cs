using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class UIServiceHandler : MonoBehaviour
{
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        // Default UI configuration setup
        _DeadMenu.SetActive(false);
        _StartWindow.SetActive(true);
        _PauseMenu.SetActive(false);
        _ResumeWindow.SetActive(false);
        setCoinCountUI(SaveManager.Data.Balance);
        HighScore = SaveManager.Data.HighScore;
    }
    // Player Score Handling
    [SerializeField]
    private TextMeshProUGUI _ScoreUI;
    [SerializeField]
    private TextMeshProUGUI _HighScoreUI;
    private int HighScore 
    {
        get => SaveManager.Data.HighScore;
        set => _HighScoreUI.text = "High Score: " + value.ToString(); 
    }
    public static UIServiceHandler instance;
    public int ScoreUI
    {
        get => int.TryParse(_ScoreUI.text, out int a) ? a : 0;
        set 
        {
            _ScoreUI.text = value.ToString();
            if (value > HighScore) HighScore = value;
#if PLATFORM_STANDALONE
            DiscordManager.SetPresence(PlayerHandler.PlrGameState, (value > HighScore) ? value : HighScore, MapRenderer.getScore);
#endif
        }
    }
    // Dead Menu Hanlding
    [SerializeField]
    private GameObject _DeadMenu;
    [SerializeField]
    private TextMeshProUGUI _DeadScoreUI;
    public void ShowDeadMenu()
    {
        _ScoreUI.gameObject.SetActive(false);
        _HighScoreUI.gameObject.SetActive(false);
        _DeadScoreUI.text = string.Format("You scored {0} points with a high score of {1} points.",MapRenderer.getScore,SaveManager.Data.HighScore);
        _DeadMenu.SetActive(true);
    }
    public void Retrybtn_Handler()
    {
        SceneManager.LoadScene("MainGame", LoadSceneMode.Single);
    }
    public void BackMenubtn_Handler()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
    // Start Window Handler
    [SerializeField]
    private GameObject _StartWindow;
    public static void closeStartWindow()
    {
        instance._StartWindow.SetActive(false);
    }
    // Pause Menu Handler
    [SerializeField]
    private GameObject _PauseMenu;
    public static bool pauseMenuVisible
    {
        get => instance._PauseMenu.activeSelf;
    }
    public void PauseKeyPressed(InputAction.CallbackContext cb)
    {
        if (isResumeWindowVisible && PlayerHandler.PlrGameState == GameState.Playing) return;
        if (cb.phase == InputActionPhase.Started && PlayerHandler.PlrGameState != GameState.Dead)
        {
            _PauseMenu.SetActive(!_PauseMenu.activeSelf);
            //Time.timeScale = _PauseMenu.activeSelf ? 0 : 1;
            if (_PauseMenu.activeSelf) { Time.timeScale = 0; return; }
            if (PlayerHandler.PlrGameState == GameState.Playing)
                StartCoroutine(RunResumeWindow());
            else
                Time.timeScale = _PauseMenu.activeSelf ? 0 : 1;
        }
    }
    public void Resumebtn_Handler()
    {
        _PauseMenu.SetActive(false);
        if (PlayerHandler.PlrGameState == GameState.Playing)
            StartCoroutine(RunResumeWindow());
        else
            Time.timeScale = _PauseMenu.activeSelf ? 0 : 1;
        //Time.timeScale = 1;
    }
    public void Exitbtn_Handler()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
    // Coin UI Handler
    [SerializeField]
    private TextMeshProUGUI _CoinsCountWindow;
    public static void setCoinCountUI(int coins)
    {
        if (!instance) return;
        instance._CoinsCountWindow.text = coins.ToString();
    }
    // Resume UI Handler
    [SerializeField]
    private GameObject _ResumeWindow;
    private IEnumerator RunResumeWindow()
    {
        _ResumeWindow.SetActive(true);
        for (int i=3;i>0;i--)
        {
            _ResumeWindow.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = string.Format("Resume in {0}...",i);
            yield return new WaitForSecondsRealtime(1f);
        }
        _ResumeWindow.SetActive(false);
        PlayerHandler.PlayerVelocity = Vector2.zero;
        Time.timeScale = 1;
    }
    public static bool isResumeWindowVisible { get => instance._ResumeWindow.activeSelf;  } 

}
