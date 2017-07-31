using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Tower")
        {
            //print("Collided with tower");
            Player.interactionObjects.Add(other.gameObject);
        }
        //else
        //{
        //    print(other.gameObject.tag);
        //}
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Tower")
        {
            //print("Left with tower");
            Player.interactionObjects.Remove(other.gameObject);
        }
    }
}
