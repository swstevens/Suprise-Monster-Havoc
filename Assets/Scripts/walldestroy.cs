using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class walldestroy : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
    	//Debug.Log(other.tag);
    	Destroy(gameObject);
    	//if (other.CompareTag("door"))
    	//{
    	//	Debug.Log("door Encountered");
    	//	Destroy(gameObject);
    	//}
    }
}
