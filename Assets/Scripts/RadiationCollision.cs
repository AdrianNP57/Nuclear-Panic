using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadiationCollision : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D coll) 
    {
        if(coll.name == "AlphaRadiation") //Alpha Collision
        {
            GameObject.Find("RadiationBar").GetComponent<RadiationBar>().alpha = true;
        }
        else if (coll.name == "BetaRadiation") //Beta Collision
        {
            GameObject.Find("RadiationBar").GetComponent<RadiationBar>().beta = true;
        }
    }
}
