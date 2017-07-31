using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitRangeController : MonoBehaviour {

    private Player player;

	// Use this for initialization
	void Start () {
         player = GameObject.Find("Player").GetComponent<Player>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        print(other.gameObject.tag);
        if (other.gameObject.tag == "EndGoal")
        {
            player.gameController.win();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "GhostAttackRange")
        {
            player.takeDamage(10);
            //print("Ouch!");

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "GhostAttackRange")
        {
            print("Left with tower");
            Player.interactionObjects.Remove(other.gameObject);
        }
    }
}
