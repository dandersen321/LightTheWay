using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour {

    Player player;
    Text startText;
    GhostFollower[] ghosts;

    bool animStarted = false;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player").GetComponent<Player>();
        startText = GameObject.Find("StartText").GetComponent<Text>();
        ghosts = GameObject.FindObjectsOfType<GhostFollower>();

        var gameController = GameObject.Find("GameController").GetComponent<GameController>();
        gameController.gameOver = true;
        foreach (GhostFollower ghost in ghosts)
        {
            ghost.aggressive = false;
            ghost.attackDistance = 0;
            ghost.initialAttackDistance = 0;
            ghost.inStart = true;
        }

        //player.anim.SetFloat("inputZ", Mathf.Abs(1))
        

        print("GameStart!");

    }
	
	// Update is called once per frame
	void Update () {

        if(Input.GetKey(KeyCode.Space))
        {
            SceneManager.LoadScene("Level1");
        }







        if (animStarted)
            return;

        animStarted = true;
        print("update!");
        foreach (GhostFollower ghost in ghosts)
        {
            //ghost.anim.SetFloat("repeat", 1);
            //ghost.aggressive = false;
            //ghost.playAnim("ghost_idle");
            ghost.anim.Play("ghost_attack", -1, 0f);
        }

        player.anim.SetFloat("inputZ", Mathf.Abs(1));

        startText.text = "Press Spacebar to Start";
        startText.enabled = true;

    }
}
