using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
	public static MapManager instance;
	void Awake()
	{
		instance = this;
	}
	public int i = 1;
	public int mapLimit = 10;
	public string nextLevel;
}
