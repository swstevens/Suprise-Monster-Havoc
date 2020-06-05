using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOptions : MonoBehaviour
{

	public GameObject options;
	private static GameObject optionsCheck;

    void Awake()
    {

        DontDestroyOnLoad(options);

        if (optionsCheck == null) {

        	optionsCheck = options;

        } else {

        	Destroy(options);
        }
    }
}