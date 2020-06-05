using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider healthbar;

    private string hubWorld;

    void Start()
    {
        hubWorld = SceneManager.GetActiveScene().name;

        if (hubWorld != "hub_world") {

            healthbar.value = PlayerManager.instance.hp;
        }

        else {

            healthbar.value = 100;
        }
    }

    void Update()
    {
        if (hubWorld != "hub_world") {

            healthbar.value = PlayerManager.instance.hp;
        }
    }

    void OnTriggerStay(Collider other)
    {
	if (other.gameObject.CompareTag("enemy"))
        {
            Debug.Log("Taking damage");

            //PlayerManager.instance.hp -= 1;
            PlayerManager.instance.hp -= 1;
            healthbar.value -= 1;
            //hp = health;

            if (PlayerManager.instance.hp < 0) {
                SceneManager.LoadScene("hub_world");
            }
    	}
    }
}
