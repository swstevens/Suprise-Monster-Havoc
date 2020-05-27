using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ElevatorController : MonoBehaviour
{
	public Text dialogue;
	private bool inRange = false;
	private string leveltoload;

    // Start is called before the first frame update
    void Start()
    {
        leveltoload = MapManager.instance.nextLevel;
    }

    void Update()
    {
    	if (inRange)
    	{
    		if (EnemyManager.instance.numEnemies >= 0)
    		{
    			if (Input.GetKeyDown(KeyCode.E))
    			{
    				dialogue.text = "Level Progressed";
    				SceneManager.LoadScene(leveltoload, LoadSceneMode.Single);
    				//load medium level, broken for now
    				SceneManager.LoadScene("hub_world");
    			}
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
        dialogue.text = "hello";
        if (EnemyManager.instance.numEnemies > 0) 
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

