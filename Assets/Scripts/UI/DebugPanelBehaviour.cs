using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugPanelBehaviour : MonoBehaviour
{
    public GameObject debugPanel;
    private GameObject debugText;

    private List<System.Tuple<string, string>> debugLines;

    // Awake
    void Awake()
    {
        debugLines = new List<System.Tuple<string, string>>();
        debugText = debugPanel.transform.GetChild(0).gameObject;

        EventManager.StartListening("InputDebug", OnDebugKeyPressed);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        string text = "";

        foreach(System.Tuple<string, string> line in debugLines)
        {
            text += $"{line.Item1}: {line.Item2}\n";
        }

        debugText.GetComponent<Text>().text = text;

        debugLines.Clear();
    }

    private void AddDebugLine(string tag, string value)
    {
        debugLines.Add(new System.Tuple<string, string>(tag, value));
    }

    private void OnDebugKeyPressed()
    {
        debugPanel.SetActive(!debugPanel.activeSelf);
    }

    public static void Log(string tag, string value)
    {
        GameObject.FindGameObjectWithTag("UI").GetComponent<DebugPanelBehaviour>().AddDebugLine(tag, value);
    }
}
