using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Target : MonoBehaviour
{
	public float health;

	void Start()
	{
		health = EnemyManager.instance.enemyHP;
	}

	public void TakeDamage(float damage)
	{
		health -= damage;
		if (health <= 0f)
		{
			Die();
		}
	}

	void Die()
	{
		EnemyManager.instance.numEnemies--;
		Destroy(gameObject);
		// spawn a weapon type
	}
}
