using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraController : MonoBehaviour {

    //public GameObject player;
    //public GameObject flashLight;
    private GameObject player;
    private Vector3 offset;
    //private Vector3 lightOffset;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        //offset = transform.position - player.transform.position;
        offset = new Vector3(0, 20f, 0);
        //lightOffset = flashLight.transform.position - player.transform.position;

    }
	
	// Update is called once per frame
	void Update () {
        transform.position = player.transform.position + offset;
        //flashLight.transform.position = player.transform.position + offset;
    }
}
