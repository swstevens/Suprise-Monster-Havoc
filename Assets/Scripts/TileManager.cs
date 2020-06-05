using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public static TileManager instance;
	void Awake()
	{
		instance = this;
	}	
	public GameObject[] verticalWalls;
	public GameObject[] horizontalWalls;
	public GameObject[] floors;
	public GameObject enemy;
	public GameObject elevator;
}
