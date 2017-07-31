using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCameraController : MonoBehaviour {

    public GameObject player;
    //public GameObject flashLight;

    private Vector3 offset;
    //private Vector3 lightOffset;

    // Use this for initialization
    void Start()
    {

        //offset = transform.position - player.transform.position;
        offset = player.transform.position - transform.position;
        //lightOffset = flashLight.transform.position - player.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = player.transform.position + offset;

        float desiredAngle = player.transform.eulerAngles.y;
        Quaternion rotation = Quaternion.Euler(0, desiredAngle, 0);
        transform.position = player.transform.position - (rotation * offset);

        transform.LookAt(player.transform.position);
        //flashLight.transform.position = player.transform.position + offset;
    }
}
