using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    void Awake() 
    {
    	instance = this;
    }

    public GameObject player;
    public int hp = 100;

    public Text health;

    void Update()
    {
    	health.text = "HP: " + hp;
    }
}
