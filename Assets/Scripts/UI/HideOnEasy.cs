using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideOnEasy : MonoBehaviour
{
    [HideInInspector]
    public static bool hidden = false;

    private void Update()
    {
        if(hidden && gameObject.activeInHierarchy)
        {
            gameObject.SetActive(false);
        }
    }
}
