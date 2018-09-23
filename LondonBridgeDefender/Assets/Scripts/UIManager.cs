using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    private static UIManager _instance;
    public static UIManager Instance
    { 
        get
        {
            if (_instance == null)
            {
                Debug.LogError("UI Manager is Null!");
            }
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    public Text score;
    public Image[] healthBars;
    public Text playerScore;

    public void UpdateLives(int livesRemaining)
    {
        for(int i =0; i <= livesRemaining; i++)
        {
            if(i == livesRemaining)
            {
                healthBars[i].enabled = false;
            }
        }
    }
    public void UpdateScore()
    {
        int num = 0;
        num += 100;
        score.text = "" + num;
    }

}
