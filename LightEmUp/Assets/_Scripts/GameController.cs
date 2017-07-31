using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;
using System;

public class GameController : MonoBehaviour {

    public Player player;
    public bool gameOver = true;
    private Text endText;
    private Text messageText;
    private bool gameWon = false;
    //public GameObject [] ghosts;
    //public List<GhostFollower> ghosts; 

    int maxLevels = 5;

    private AudioSource winSound;

    // Use this for initialization
    void Start () {

        player = GameObject.Find("Player").GetComponent<Player>();
        //ghosts = new List<GhostFollower>(GameObject.FindObjectsOfType<GhostFollower>());
        endText = GameObject.Find("EndText").GetComponent<Text>();
        messageText = GameObject.Find("MessageText").GetComponent<Text>();
        //ghosts = GameObject.FindGameObjectsWithTag("Ghost");
        GameObject mainCamera = GameObject.Find("Main Camera");
        var playerPosition = player.transform.position;
        mainCamera.transform.position = new Vector3(playerPosition.x + 5, playerPosition.y + 20, playerPosition.z);

        winSound = GetComponent<AudioSource>();


    }

    // Update is called once per frame
    void Update()
    {
        if(!gameOver)
        {
            return;
        }

        if(Input.GetKey(KeyCode.Space))
        {
            Scene scene = SceneManager.GetActiveScene();
            string nextLevel;
            if (gameWon)
            {
                Player.timesDied = 0;
                // yep, this is ugly but time crunch bro!
                int levelNumber = getLevelNumber();
                nextLevel = "Level" + (levelNumber + 1);
                print(nextLevel);
                if(levelNumber >= maxLevels)
                {
                    nextLevel = "EndScreen";
                }
            }
            else
            {
                nextLevel = scene.name;
            }
            //Destroy(GameObject.FindGameObjectWithTag("Player"));
            //print("Next Level: " + nextLevel);
            SceneManager.LoadScene(nextLevel);
        }
    }

    private int getLevelNumber()
    {
        Scene scene = SceneManager.GetActiveScene();
        return int.Parse(new string(scene.name.Where(c => char.IsDigit(c)).ToArray()));
    }

    public void lose()
    {
        endText.text = "You failed valiantly";
        messageText.text = "Press Spacebar to restart";
        end();
    }

    public void win()
    {
        player.winAnimate();
        endText.text = "Level " + getLevelNumber() + " of 5 Complete";
        messageText.text = "Press Spacebar to continue";
        gameWon = true;
        winSound.Play();
        end();
    }

    public void end()
    {
        gameOver = true;
        endText.enabled = true;
        messageText.enabled = true;
        foreach (GhostFollower ghost in GameObject.FindObjectsOfType<GhostFollower>())
        {
            ghost.die();
        }
    }
}
