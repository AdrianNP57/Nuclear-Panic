using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LethalRadiationAnchorBehaviour : MonoBehaviour
{
    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = Camera.main.ViewportToWorldPoint(new Vector3(0, 0.5f, 0));
    }
}
