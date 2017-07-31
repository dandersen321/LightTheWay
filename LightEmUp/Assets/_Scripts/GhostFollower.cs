using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostFollower : MonoBehaviour {

    public Transform playerTransform;
    public float speed;

    private NavMeshAgent agent;
    private float extraRotationSpeed = 100;

    private int health = 60;
    float timeUntilVunerable = 0f;
    float damageInvunerabilityTime = 0.25f;

    public bool alive = true;
    public Animator anim;

    public bool aggressive = true;
    public bool inStart = false;

    private Transform ghostTransform;
    private float shrinkSpeed = 100f;

    public float attackDistance = 20;
    public float initialAttackDistance = 10;
    private bool attacking = false;

    private GameController gameController;

    private AudioSource deathSound;


    // Use this for initialization
    void Start () {

        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        agent.acceleration = speed*5;
        agent.angularSpeed = 9999;
        //agent.angularSpeed = speed;
        agent.autoBraking = false;
        anim = GetComponent<Animator>();
        //agent.
        ghostTransform = GetComponent<Transform>();
        //playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        deathSound = GetComponent<AudioSource>(); 

    }
	
	// Update is called once per frame
	void Update ()
    {

        if(inStart)
        {
            return;
        }

        if(!alive)
        {
            if (transform.localScale.x > 0.1f)
            {
                agent.Stop();
                ghostTransform.localScale -= Vector3.one * Time.deltaTime * shrinkSpeed;
            }
        }
        else if(!gameController.gameOver && aggressive && 
            (attacking && Vector3.Distance(transform.position, playerTransform.position) <= attackDistance) ||
            (Vector3.Distance(transform.position, playerTransform.position) <= initialAttackDistance))
        {
            //if()
            //{

            //}
            //else if(Vector3.Distance(transform.position, player.position) <= initialAttackDistance)
            //{

            //}
            attacking = true;
            //print("Distance: " + Vector3.Distance(transform.position, playerTransform.position).ToString());
            agent.Resume();
            agent.SetDestination(playerTransform.position);
            extraRotation();
        }
        else
        {
            attacking = false;
            agent.Stop();
        }



    }

    void extraRotation()
    {
        Vector3 lookrotation = agent.steeringTarget - transform.position;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookrotation), extraRotationSpeed * Time.deltaTime);

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Tower" && other.transform.Find("Point light").gameObject.GetComponent<Light>().enabled)
        {
            takeDamage(10);
        }
    }

    public void takeDamage(int damage)
    {

        if (timeUntilVunerable > Time.time || !alive)
        {
            return;
        }
        timeUntilVunerable = Time.time + damageInvunerabilityTime;

        health -= damage;

        deathSound.volume = 0.7f;
        deathSound.Play();

        if (health <= 0)
        {
            die();
        }
        else
        {
            anim.Play("ghost_damage", -1, 0f);
        }

    }

    public void playAnim(string animName)
    {
        anim.Play("ghost_die", -1, 0f);
    }

    public void die()
    {
        anim.Play("ghost_die", -1, 0f);
        alive = false;
    }
}
