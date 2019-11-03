using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Score : MonoBehaviour
{
    public GameObject player;
    public GameObject levelPool;
    public TextMeshProUGUI scoreText;

    private int scoreSubract;
    private int currentScore = 0;

    private bool countScore;

    void Awake()
    {
        EventManager.StartListening("DifficultyChosen", OnDifficultyChosen);
        Init();
    }

    void Init()
    {
        countScore = false;
    }

    void Update()
    {
        scoreText.text = CurrentScore().ToString("0"); //sets scoreText to an integer depending on the x position of the player
    }

    public int CurrentScore()
    {
        // TODO if can be simplified
        if (!player.GetComponent<PlayerBehaviour>().isDead && countScore)
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
}
