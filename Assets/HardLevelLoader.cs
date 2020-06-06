using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HardLevelLoader : MonoBehaviour
{
    
    private bool inRange;
    private bool active = true;
    public Transform player;
    public Text dialogue;

    // Start is called before the first frame update
    void Start()
    {
        active = true;
        inRange = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (inRange)
        {
            //player = PlayerManager.instance.player.transform;
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 toOther = player.position - transform.position;
            Debug.Log(Vector3.Dot(forward,toOther));
            if (Vector3.Dot(forward,toOther) < 5)
            {
                if (Input.GetKeyDown(KeyCode.E) && active == true)
                {

                    //harScore.SetActive(true);
                    TimeCounter.instance.currentLevel = 3;
	            	SceneManager.LoadScene("hard_level");
                }
            }
            // if looking at the terminal (dot product)
            // let the player press e to heal for 50
            // maybe play a sound when e gets pressed
            // also have a popup that says "Press E to Heal"
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered trigger");
            inRange = true;
        }
        if (active) 
        {
            dialogue.text = "Level 3 (E)";
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
