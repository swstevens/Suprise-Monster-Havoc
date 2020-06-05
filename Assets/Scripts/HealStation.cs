using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealStation : MonoBehaviour
{
    private bool inRange;
    private bool active = true;
    public Transform player;
    private Text dialogue;
    public GameObject stationLight;
    public Slider healthbar;
	public AudioSource healSound;

    // Start is called before the first frame update
    void Start()
    {
        dialogue = MapManager.instance.dialogue;
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
            Debug.Log(Vector3.Dot(forward.normalized,toOther.normalized));
            if (Vector3.Dot(forward,toOther) >= .5)
            {
                if (Input.GetKeyDown(KeyCode.E) && active == true)
                {

                	if (PlayerManager.instance.hp >= 100) {

                		dialogue.text = "HP Already Full";
                		// for debugging
                		//PlayerManager.instance.hp -= 50;

                	} else {

                		Debug.Log("Player has been healed");
	                    healthbar.value+= 50;

                        PlayerManager.instance.hp += 50;
	                    if (PlayerManager.instance.hp >100) {

	                        PlayerManager.instance.hp = 100;
	                    }

	                    //healSound.Play();
	                    dialogue.text = "You have been healed!";
	                    active = false;
                	}  

                	if (active == false) {

                		stationLight.SetActive(false);
                	}
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
            dialogue.text = "Heal (E)";
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
