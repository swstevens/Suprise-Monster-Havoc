using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    // Start is called before the first frame update
    void OnTriggerEnter(Collider other)
    {
    	// Destroy(other.gameObject);
		if (other.CompareTag("SpawnPoint"))
        {
        	Destroy(other.gameObject);
        	Debug.Log("Destroyed a node");
        }
    }
}
