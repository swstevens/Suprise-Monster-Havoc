using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
    	if (other.CompareTag("wall"))
    	{
    		Debug.Log("Wall Encountered");
    		Destroy(other.transform);
    	}
    }
}
