using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door : MonoBehaviour
{
    void OnTriggerStay(Collider other)
    {
    	Debug.Log(other.tag);
    	if (other.CompareTag("wall"))
    	{
    		Debug.Log("Wall Encountered");
    		other.transform.gameObject.SetActive(false);
    	}
    }
}
