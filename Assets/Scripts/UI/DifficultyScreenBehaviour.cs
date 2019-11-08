using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO prevent premature interaction
public class DifficultyScreenBehaviour : MonoBehaviour
{

    // Start is called before the first frame update
    void Awake()
    {
        Init();
    }

    void Init()
    {
        EventManager.StartListening("InputGlassesUp", OnEasyChosen);
        EventManager.StartListening("InputJumpUp", OnHardChosen);
    }

    private void OnDifficultyChosen()
    {
        gameObject.SetActive(false);

        EventManager.StopListening("InputGlassesUp", OnEasyChosen);
        EventManager.StopListening("InputJumpUp", OnHardChosen);

        EventManager.TriggerEvent("DifficultyChosen");
    }

    private void OnEasyChosen()
    {
        Debug.Log("Easy");

        EventManager.TriggerEvent("EasyDifficultyChosen");
        OnDifficultyChosen();
    }

    private void OnHardChosen()
    {
        Debug.Log("Hard");

        EventManager.TriggerEvent("HardDifficultyChosen");
        OnDifficultyChosen();
    }
}