using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ScoreBehaviour : MonoBehaviour
{
    public GameObject player;
    public GameObject levelPool;
    public TextMeshProUGUI scoreText;

    private int scoreSubract;
    private int currentScore;

    private bool countScore;

    void Awake()
    {
        EventManager.StartListening("DifficultyChosen", OnDifficultyChosen);
        EventManager.StartListening("PlayerDied", OnPlayerDied);
        EventManager.StartListening("GameRestart", Init);

        Init();
    }

    void Init()
    {
        countScore = false;
        currentScore = 0;
        scoreSubract = 0;
    }

    void Update()
    {
        scoreText.text = CurrentScore().ToString("0"); //sets scoreText to an integer depending on the x position of the player
    }

    public int CurrentScore()
    {
        if (countScore)
        {
            currentScore = ((int)player.transform.position.x) - scoreSubract > 0 ? ((int)player.transform.position.x) - scoreSubract : 0;
        }

        return currentScore;
    }

    private void OnDifficultyChosen()
    {
        countScore = true;
        scoreSubract = levelPool.GetComponent<LevelPoolManager>().CurrentEndOfWorld();
    }

    private void OnPlayerDied()
    {
        countScore = false;
    }
}
