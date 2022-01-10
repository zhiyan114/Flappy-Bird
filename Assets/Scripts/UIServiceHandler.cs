using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

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
    }
    // Player Score Handling
    [SerializeField]
    private TextMeshProUGUI _ScoreUI;
    public static UIServiceHandler instance;
    public int ScoreUI
    {
        get => int.TryParse(_ScoreUI.text, out int a) ? a : 0;
        set => _ScoreUI.text = value.ToString();
    }
    // Dead Menu Hanlding
    [SerializeField]
    private GameObject _DeadMenu;
    [SerializeField]
    private TextMeshProUGUI _DeadScoreUI;
    public void ShowDeadMenu()
    {
        _DeadScoreUI.text = string.Format("You scored {0} points with a high score of {1} points.",MapRenderer.getScore,SaveManager.Data.HighScore);
        _DeadMenu.SetActive(true);
    }
    public void Retrybtn_Handler()
    {
        SceneManager.LoadScene("MainGame", LoadSceneMode.Single);
    }
}
