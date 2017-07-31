using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndController : MonoBehaviour {

    GameController gameController;
    // Use this for initialization
    GameObject endGoal;
	void Start () {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        endGoal = GetComponent<GameObject>();
        //var render = GetComponent<Renderer>();
        //var material = GetComponent<Material>();
        
        //endGoal.renderer.
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        print(other.tag);
        if(other.tag == "Player")
        {
            gameController.win();
        }
    }
}
