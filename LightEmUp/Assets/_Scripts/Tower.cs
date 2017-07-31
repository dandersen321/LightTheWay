using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {

    private const float towerLifeLength = 15f;
    private float towerLifeStart = 0f;
    private float towerRange = 100f;

    Light towerLight;

	// Use this for initialization
	void Start () {
        //SphereCollider sphereCollider = GetComponent<SphereCollider>();

        towerLight = transform.Find("Point light").gameObject.GetComponent<Light>();
        
    }

    public void turnLightOn()
    {
        //towerLifeLeft = towerLifeLength;
        towerLifeStart = Time.time;
        towerLight.enabled = true;
    }
	
	// Update is called once per frame
	void Update () {

        if(!towerLight.enabled)
        {
            return;
        }

        float towerLifeLeft = towerLifeLength - (Time.time - towerLifeStart);

        print("Time.time: " + Time.time.ToString());
        print("TowerLifeLeft " + towerLifeLeft.ToString());

        if(towerLifeLeft <= 0)
        {
            towerLight.enabled = false;
        }
        else
        {
            towerLight.range = (towerLifeLeft / towerLifeLength) * towerRange;
        }
            

    }

}
