using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
	public static EnemyManager instance;
	void Awake()
	{
		instance = this;
	}
	public int numEnemies = 0;
	public float enemyHP = 0f;
	public Text enemyCount;

	void Update()
	{
		enemyCount.text = "Enemies Left: " + numEnemies;
	}
}
