using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadiationCollision : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D coll) 
    {
        if(coll.gameObject.tag == "LowRadiation") //Low Collision
        {
            GameObject.Find("RadiationBar").GetComponent<RadiationBar>().lowRadiation = true;
        }
        else if (coll.gameObject.tag == "MediumRadiation") //Medium Collision
        {
            GameObject.Find("RadiationBar").GetComponent<RadiationBar>().mediumRadiation = true;
        }
    }
    public void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "LowRadiation") //Alpha Collision Exit
        {
            GameObject.Find("RadiationBar").GetComponent<RadiationBar>().lowRadiationOut = true;
        }
        else if (coll.gameObject.tag == "MediumRadiation") //Beta Collision Exit
        {
            GameObject.Find("RadiationBar").GetComponent<RadiationBar>().mediumRadiationOut = true;
        }
    }
}
