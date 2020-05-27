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
	public float enemyHP = 50f;
	public Text enemyCount;
	public int damage = 10;

	void Update()
	{
		enemyCount.text = "Enemies Left: " + numEnemies;
	}
}
