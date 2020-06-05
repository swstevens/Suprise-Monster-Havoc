using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{

	public GameObject ehiscoreDisplay;
	private static GameObject edisplayCheck;

    void Awake()
    {

        DontDestroyOnLoad(ehiscoreDisplay);

        if (edisplayCheck == null) {

        	edisplayCheck = ehiscoreDisplay;

        } else {

        	Destroy(ehiscoreDisplay);
        }
    }
}