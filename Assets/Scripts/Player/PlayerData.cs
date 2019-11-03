using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [HideInInspector]
    public float currentSpeed;

    // Singleton
    public static PlayerData instance;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        
    }
}
