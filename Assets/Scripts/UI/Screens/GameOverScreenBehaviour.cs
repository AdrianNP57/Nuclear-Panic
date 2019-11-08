using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScreenBehaviour : MonoBehaviour
{
    public GameObject gameOverPanel;
    public GameObject restartText;

    // Start is called before the first frame update
    void Awake()
    {
        EventManager.StartListening("PlayerDied", OnPlayerDied);
    }

    private IEnumerator ActiveGameOverPanel()
    {
        gameOverPanel.SetActive(true);
        restartText.SetActive(false);

        yield return new WaitForSeconds(2.0f);

        restartText.SetActive(true);
        EventManager.StartListening("InputJumpUp", OnRestartPressed);
    }

    private void OnPlayerDied()
    {
        StartCoroutine(ActiveGameOverPanel());
    }

    private void OnRestartPressed()
    {
        gameOverPanel.SetActive(false);

        EventManager.StopListening("InputJumpUp", OnRestartPressed);
        EventManager.TriggerEvent("GameRestart");
    }

    void Update() { }
}
