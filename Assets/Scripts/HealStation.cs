using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealStation : MonoBehaviour
{
    private bool inRange;
    private bool active = true;
    private Transform player;
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
            player = PlayerManager.instance.player.transform;
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 toOther = player.position - transform.position;
            Debug.Log(Vector3.Dot(forward,toOther));
            if (Vector3.Dot(forward,toOther) < 1.5)
            {
                if (Input.GetKeyDown(KeyCode.E) && active == true)
                {
                    Debug.Log("Player has been healed");
                    PlayerManager.instance.hp += 50;
                    if (PlayerManager.instance.hp >100)
                    {
                        PlayerManager.instance.hp = 100;
                    }
                    dialogue.text = "You have been healed!";
                    active = false;
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
            dialogue.text = "Get close to the monitor and press E to heal!";
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
