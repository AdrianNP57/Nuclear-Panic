using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HighScoreBehaviour : MonoBehaviour
{
    public Score scoreBehaviour;
    private TextMeshProUGUI text;
    private static int highScore;

    public bool hardMode = false;

    void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        text.text = highScore.ToString("0");
    }

    // Update is called once per frame
    void Update()
    {
        if(scoreBehaviour.CurrentScore() > highScore && hardMode)
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

    private void OnDisable()
    {
        PlayerPrefs.SetInt("HighScore", highScore);
    }
}
