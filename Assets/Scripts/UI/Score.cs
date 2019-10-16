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

    void Update()
    {
        scoreText.text = CurrentScore().ToString("0"); //sets scoreText to an integer depending on the x position of the player
    }

    public int CurrentScore()
    {
        if(player.GetComponent<PlayerBehaviour>().chooseDifficulty)
        {
            scoreSubract = levelPool.GetComponent<LevelPoolManager>().CurrentEndOfWorld();
            return 0;
        }
        else
        {
            return ((int)player.transform.position.x) - scoreSubract > 0? ((int)player.transform.position.x) - scoreSubract : 0;
        }
    }
}
