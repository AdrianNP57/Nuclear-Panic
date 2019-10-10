using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menus : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("LoadingScene");
    }
    public void Quit()
    {
        Debug.Log("Game Ended");
        Application.Quit();
    }

}
