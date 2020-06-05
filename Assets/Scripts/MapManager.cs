using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
	public static MapManager instance;
	void Awake()
	{
		instance = this;
	}
	public string currentlevel;
	public string nextLevel;
	public Text dialogue;
}
