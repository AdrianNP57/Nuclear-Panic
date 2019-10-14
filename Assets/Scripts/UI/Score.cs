using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Score : MonoBehaviour
{
    public Transform player;
    public TextMeshProUGUI scoreText;

    void Update()
    {
        scoreText.text = CurrentScore().ToString("0"); //sets scoreText to an integer depending on the x position of the player

        //scoreText.text = (Int32.Parse(CurrentScore().ToString("0"))*10).ToString(); //sets scoreText to an integer depending on the 10*x position of the player
    }

    public int CurrentScore()
    {
        return (int) player.position.x;
    }
}
