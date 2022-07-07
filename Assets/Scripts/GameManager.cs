using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public enum GAME_STATUS
    {
        beforStart,
        inGame,
        gameOver
    }

    private GAME_STATUS _status;
    public GAME_STATUS status
    {
        get 
        { 
            return _status; 
        }
        set
        {
            _status = value;
            UpdateUI();
        }
    }

    public GameObject beforStart;
    public GameObject inGame;
    public GameObject gameOver;

    public Slider blood;
    public Player player;

    public BinsManager binsManager;

    public Text scoreText;
    public Text scoreTextOver;
    public Text highestText;

    void Start()
    {
        player.onDeath += GameOver;
        status = GAME_STATUS.beforStart;
        blood.maxValue = player.blood;
    }

    void Update()
    {
        blood.value= Mathf.Lerp(player.blood, blood.value, 0.5f);
    }

    private void UpdateUI()
    {
        this.beforStart.SetActive(GAME_STATUS.beforStart == status);
        this.inGame.SetActive(GAME_STATUS.inGame == status);
        this.gameOver.SetActive(GAME_STATUS.gameOver == status);
    }

    public void StartButtonOnClick()
    {
        status = GAME_STATUS.inGame;
        binsManager.Init();
    }

    public void GameOver()
    {
        status=GAME_STATUS.gameOver;
        binsManager.DestroyAll();
        scoreTextOver.text = scoreText.text;
        int score = Convert.ToInt32(scoreTextOver.text);
        int highest = 0;
        if(PlayerPrefs.HasKey("Score"))
            highest = PlayerPrefs.GetInt("Score", score);
        if (score > highest)
        {
            Debug.Log("UYes");
            highest = score;
            PlayerPrefs.SetInt("Score", highest);
        }
        highestText.text = highest.ToString();

    }
}
