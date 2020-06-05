using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Target : MonoBehaviour
{
	public float health;
	public AudioSource deathSound;

    Animator anim;

	int damageHash = Animator.StringToHash ("Take Damage");
    int dieHash = Animator.StringToHash ("Die");

	void Start()
	{
		health = EnemyManager.instance.enemyHP;
		anim = GetComponent<Animator>();
	}

	public void TakeDamage(float damage) {

		anim.ResetTrigger(damageHash);

		health -= damage;

		if (health <= 0f)
		{

			deathSound.Play();
			Die();

		} else {

			anim.SetTrigger(damageHash);
		}
	}

	void Die()
	{

		anim.SetTrigger(dieHash);
		EnemyManager.instance.numEnemies--;
		Destroy(gameObject);
	}
}
