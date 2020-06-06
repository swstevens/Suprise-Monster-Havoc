using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private float startTime;
    private float time;
    // Start is called before the first frame update
    void Start()
    {
       	startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
    	time = Time.time - startTime;
        TimeCounter.instance.levelTime = time;
    }
}
