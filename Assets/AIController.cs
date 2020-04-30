using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AIController : MonoBehaviour {

    public Transform Player;
    int MoveSpeed = 4;
    int MaxDist = 20;
    //int MinDist = 5;
 
    void Update() 
    {
        transform.LookAt(Player);
 
        if (Vector3.Distance(transform.position, Player.position) <= MaxDist) {
 
            transform.position += transform.forward * MoveSpeed * Time.deltaTime;

        }
    }
}
