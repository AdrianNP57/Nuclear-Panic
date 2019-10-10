using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadiationCollision : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D coll) 
    {
        if(coll.gameObject.tag == "Alpha") //Alpha Collision
        {
            GameObject.Find("RadiationBar").GetComponent<RadiationBar>().alpha = true;
        }
        else if (coll.gameObject.tag == "Beta") //Beta Collision
        {
            GameObject.Find("RadiationBar").GetComponent<RadiationBar>().beta = true;
        }
    }
    public void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Alpha") //Alpha Collision Exit
        {
            GameObject.Find("RadiationBar").GetComponent<RadiationBar>().alphaOut = true;
        }
        else if (coll.gameObject.tag == "Beta") //Beta Collision Exit
        {
            GameObject.Find("RadiationBar").GetComponent<RadiationBar>().betaOut = true;
        }
    }
}
