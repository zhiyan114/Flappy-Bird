using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIServiceHandler : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _ScoreUI;
    public static UIServiceHandler instance;
    private void Awake()
    {
        instance = this;
    }
    public int ScoreUI
    {
        get => int.TryParse(_ScoreUI.text, out int a) ? a : 0;
        set => _ScoreUI.text = value.ToString();
    }
}
