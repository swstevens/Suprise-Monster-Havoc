using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeCounter : MonoBehaviour
{
	public static TimeCounter instance;
	void Awake()
	{
		instance = this;
		DontDestroyOnLoad(this.gameObject);
	}
	public float level1_record = 0f;
	public float level2_record = 0f;
	public float level3_record = 0f;
	public float levelTime = 0f;
	public float currentLevel = 0;
	public Text timer;
	//0 hub, 1 easy, 2 medium, 3 hard

	void Update()
	{
		if (currentLevel == 0)
		{
			timer.text = "Level 1: " + level1_record + " | Level 2: " + level2_record + " | Level 3: " + level3_record;
		}
		else
		{
			timer.text = "Time: " + levelTime;
		}
	}

}
