using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyMedium : MonoBehaviour
{

	public GameObject mhiscoreDisplay;
	private static GameObject mdisplayCheck;

    void Awake()
    {

        DontDestroyOnLoad(mhiscoreDisplay);

        if (mdisplayCheck == null) {

        	mdisplayCheck = mhiscoreDisplay;

        } else {

        	Destroy(mhiscoreDisplay);
        }
    }
}