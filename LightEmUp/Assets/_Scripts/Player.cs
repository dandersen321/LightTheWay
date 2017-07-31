using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    public Animator anim;
    public float speed;

    private Rigidbody rb;

    // TODO: Find better way to implement this when time permits
    public static List<GameObject> interactionObjects = new List<GameObject>();
    public static int health;
    public static int timesDied = 0;

    float timeUntilVunerable = 0f;
    float damageInvunerabilityTime = 0.5f;

    private GameObject playerGameObject;
    private Renderer render;

    //public bool alive;
    public GameController gameController;

    private AudioSource playerHitSound;

    private Text messageText;
    private Text playerHealth;
    // Use this for initialization
    void Start () {
        //print("Player Start");
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        //alive = true;
        gameController = GameObject.Find("GameController").GetComponent <GameController>();

        playerGameObject = GetComponent<GameObject>();
        render = GetComponent<Renderer>();
        playerHitSound = GetComponent<AudioSource>();

        Player.health = 100 + timesDied * 10;

        try
        {
            if (!gameController.gameOver)
            {
                messageText = GameObject.Find("MessageText").GetComponent<Text>();
                playerHealth = GameObject.Find("PlayerHealth").GetComponent<Text>();
                playerHealth.enabled = true;
            }
        }
        catch(Exception e)
        {
            // todo find out why this crashes
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameController.gameOver)
        {
            return;
        }
        playerHealth.text = "Health: " + health.ToString();

        

        

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);
        rb.velocity = movement * speed;
        //rb.AddForce(movement * speed);

        //rigidbody.position = new Vector3(rigidbody.position.x + moveHorizontal, 0, rigidbody.position.z + moveVertical);
        //print("moveHorizontal" + moveHorizontal.ToString());
        //print("moveVertical" + moveVertical.ToString());
        if (movement != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(movement);
        }
        //else
        //{
        //    transform.rotation = transform.rotation;
        //}

        //if (Input.GetKeyDown("1"))
        //{
        //    print("I pressed one");
        //    anim.Play("POSE13", -1, 0f);
        //}

        //if (Input.GetKeyDown("w"))
        //{
        //    print("I pressed w");
        //    anim.Play("RUN00_F", -1, 0f);
        //}
        //else if (Input.GetKeyDown("a"))
        //{
        //    print("I pressed a");
        //    anim.Play("RUN00_F", -1, 0f);
        //}
        //else if (Input.GetKeyDown("d"))
        //{
        //    print("I pressed d");
        //    anim.Play("RUN00_F", -1, 0f);
        //}
        //else if (Input.GetKeyDown("s"))
        //{
        //    print("I pressed d");
        //    anim.Play("RUN00_F", -1, 0f);
        //}

        if (Input.GetKey(KeyCode.Space))
        {
            foreach(GameObject obj in interactionObjects)
            {
                print(obj.tag);
                if(obj.tag == "Tower")
                {
                    Tower tower = obj.GetComponent<Tower>();
                    tower.turnLightOn();

                    //Light light = obj.transform.Find("Point light").gameObject.GetComponent<Light>();
                    ////light.enabled = !light.enabled;
                    //if(!light.enabled)
                    //    light.enabled = true;

                    //foreach (Light light in obj.GetComponents<Light>())
                    //{
                    //    print(light.tag);
                    //    if(light.tag == "TowerLight")
                    //    {
                    //        light.enabled = !light.enabled;
                    //    }
                    //}
                }
            }
        }

        bool inactiveLightFound = false;
        foreach (GameObject obj in interactionObjects)
        {
            if (obj.tag == "Tower")
            {
                inactiveLightFound = true;
                //Light light = obj.transform.Find("Point light").gameObject.GetComponent<Light>();
                ////light.enabled = !light.enabled;
                //if (!light.enabled)
                //{
                //    inactiveLightFound = true;
                //    break;
                //}

            }
        }

        if(inactiveLightFound)
        {
            messageText.text = "Press Spacebar to turn on Tower Light (hurts ghosts)";
            messageText.enabled = true;
        }
        else
        {
            messageText.enabled = false;
        }

        // Shift + Q == quit
        if((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && Input.GetKey(KeyCode.Q))
        {
            Application.Quit();
        }




        anim.SetFloat("inputX", Mathf.Abs(moveHorizontal));
        anim.SetFloat("inputZ", Mathf.Abs(moveVertical));

    }

    public void winAnimate()
    {
        anim.SetFloat("inputX", 0);
        anim.SetFloat("inputZ", 0);
        anim.Play("WIN00");
        rb.velocity = Vector3.zero;
        
    }

    public void takeDamage(int damage)
    {
        
        if (timeUntilVunerable > Time.time || gameController.gameOver)
        {
            return;
        }
        timeUntilVunerable = Time.time + 0.5f;

        health -= damage;
        playerHealth.text = "Health: " + health.ToString();

        //render.material.color = Color.red;
        //GameObject a  = GameObject.Find("mesh_root");
        ////Shader[] renders = a.GetComponentsInChildren<Shader>();
        //var renders = a.transform;
        //foreach(var r in renders)
        //{
        //    print(r.ToString());
        //}


        playerHitSound.Play();

        if (health <= 0)
        {
            anim.Play("DAMAGED01", -1, 0f);
            timesDied += 1;
            gameController.lose();
        }
        else
        {
            anim.Play("DAMAGED00", -1, 0f);
        }

    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    print(other.gameObject.tag);
    //    if (other.gameObject.tag == "Ghost")
    //    {
    //        print("Ouch!");
    //        anim.Play("DAMAGED00", -1, 0f);
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.tag == "Ghost")
    //    {
    //        print("Left with tower");
    //        Player.interactionObjects.Remove(other.gameObject);
    //    }
    //}

    //void OnCollisionEnter(Collision collision)
    //{
    //    foreach (ContactPoint contact in collision.contacts)
    //    {
    //        var colName = contact.thisCollider.name;
    //        switch (colName)
    //        {
    //            case "Rotor":
    //                //Do something
    //                break;
    //            case "LandingGear":
    //                //Do  something
    //                break;
    //            case "Body":
    //                //Do something
    //                break;
    //        }
    //    }
    //}
}
