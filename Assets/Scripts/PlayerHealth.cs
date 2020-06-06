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

    public AudioSource hitSounds;
    private float hitSoundsTimer = 0.5f;

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
            //Debug.Log("Taking damage");
            hitSoundsTimer -= Time.deltaTime;

            if (hitSoundsTimer <= 0.1f) {
                hitSounds.Play();
                hitSoundsTimer = 0.5f;
            }
            //PlayerManager.instance.hp -= 1;
            PlayerManager.instance.hp -= 1;
            healthbar.value -= 1;
            //hp = health;

            if (PlayerManager.instance.hp < 0) {
            	TimeCounter.instance.currentLevel = 0;
                SceneManager.LoadScene("hub_world");
            }
    	}
    }
}
