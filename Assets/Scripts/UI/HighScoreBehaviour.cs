﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HighScoreBehaviour : MonoBehaviour
{
    public Score scoreBehaviour;
    private TextMeshProUGUI text;
    private static int highScore = -1;

    void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if(scoreBehaviour.CurrentScore() > highScore)
        {
            highScore = scoreBehaviour.CurrentScore();
            text.text = highScore.ToString("0");
        }

        if(Input.GetKey("r"))
        {
            highScore = 0;
            text.text = highScore.ToString("0");
        }
    }
}
