using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniversalInput : MonoBehaviour
{
    
    public static bool GetButtonDown(string button)
    {
        bool result = Input.GetButtonDown(button);

        if(!result)
        {
            result = GetTouchInput(button, TouchPhase.Began);
        }

        return result;
    }

    public static bool GetButtonUp(string button)
    {
        bool result = Input.GetButtonUp(button);

        if(!result)
        {
            result = GetTouchInput(button, TouchPhase.Ended);
        }

        return result;
    }

    public static bool GetButton(string button)
    {
        bool result = Input.GetButton(button);

        if(!result)
        {
            result = GetTouchInput(button, null);
        }

        return result;
    }

    private static bool GetTouchInput(string button, TouchPhase? phase)
    {
        bool result = false;

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (phase == null || touch.phase == TouchPhase.Began)
            {
                if (button == "Glasses")
                {
                    result = Camera.main.ScreenToViewportPoint(touch.position).x < 0.5;
                }
                else if (button == "Jump")
                {
                    result = Camera.main.ScreenToViewportPoint(touch.position).x > 0.5;
                }
            }
        }

        return result;
    }
}
