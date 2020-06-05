using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyHard : MonoBehaviour
{

	public GameObject hhiscoreDisplay;
	private static GameObject hdisplayCheck;

    void Awake()
    {

        DontDestroyOnLoad(hhiscoreDisplay);

        if (hdisplayCheck == null) {

        	hdisplayCheck = hhiscoreDisplay;

        } else {

        	Destroy(hhiscoreDisplay);
        }
    }
}