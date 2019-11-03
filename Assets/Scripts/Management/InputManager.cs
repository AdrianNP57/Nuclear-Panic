using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonUp("Jump"))
        {
            EventManager.TriggerEvent("InputJumpUp");
        }
        if(Input.GetButtonUp("Glasses"))
        {
            EventManager.TriggerEvent("InputGlassesUp");
        }

        if (Input.GetButton("Jump"))
        {
            EventManager.TriggerEvent("InputJump");
        }
        if (Input.GetButton("Glasses"))
        {
            EventManager.TriggerEvent("InputGlasses");
        }
    }
}
