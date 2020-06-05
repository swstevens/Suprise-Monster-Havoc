using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NewGeneratorLoader : MonoBehaviour
{
	public Text dialogue;
	private bool inRange = false;
	private string leveltoload;

    // Start is called before the first frame update
    void Start()
    {
        //leveltoload = MapManager.instance.nextLevel;
        //dialogue.text = "";
    }

    void Update()
    {
    	if (inRange)
    	{
    		if (Input.GetKeyDown(KeyCode.E))
    			{
    				
    				SceneManager.LoadScene("new_map_gen");
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
        dialogue.text = "Check out our sick new map generator";
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

