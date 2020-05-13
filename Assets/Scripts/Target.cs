using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
	public float health = 20f;

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
		Destroy(gameObject);
		// spawn a weapon type
	}
}
