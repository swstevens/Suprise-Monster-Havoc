using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ElevatorController : MonoBehaviour
{
	private Text dialogue;
	private bool inRange = false;
	private string leveltoload;

	public AudioSource teleportSound;
	private float teleportTimer = 4f;
    private float teleportTimerStart;
    private bool teleportInitiated;

    Animator anim;

	int shootHash = Animator.StringToHash ("Shoot");
    //int dieHash = Animator.StringToHash ("Die");
    // Start is called before the first frame update
    void Start()
    {
        dialogue = MapManager.instance.dialogue;
        leveltoload = MapManager.instance.nextLevel;
        dialogue.text = "";
    }

    void Update()
    {
    	if (inRange)
    	{
    		if (EnemyManager.instance.numEnemies <= 0)
    		{
    			if (Input.GetKeyDown(KeyCode.E))
    			{

    				teleportInitiated = true;
    				teleportSound.Play();
    			}
    		}
    	}

    	if (teleportInitiated) {

    		dialogue.text = "Level Cleared!";
			teleportTimer -= Time.deltaTime;

			if (teleportTimer <= 2) {

				dialogue.text = "Teleporting...";
			}

			if (teleportTimer <= 0) {

				teleportInitiated = false;
				teleportTimer = 4;
				SceneManager.LoadScene("hub_world");
			}
    	}
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered trigger");
            inRange = true;
        }
        
        //dialogue.text = "hello";
        if (EnemyManager.instance.numEnemies > 1) 
        {
            dialogue.text = "Defeat all Enemies to progress to the next level!";
        }
        else if (EnemyManager.instance.numEnemies == 0)
        {
        	dialogue.text = "Press E to progress to the next level";
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player left trigger");
            inRange = false;
            dialogue.text = "";
        }
    }
}

