using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Target : MonoBehaviour
{
	public float health;
	public AudioSource deathSound;

	public float hitTimer = 0.5f;
	public bool hit;

	public float dieTimer = 1.0f;
	public bool dead;

    Animator anim;

	int damageHash = Animator.StringToHash ("Take Damage");
    int dieHash = Animator.StringToHash ("Die");

	void Start()
	{
		health = EnemyManager.instance.enemyHP;
		anim = GetComponent<Animator>();
		dead = false;
	}

	void Update() {

		if (dead == true) {

			dieTimer -= Time.deltaTime;

			if (dieTimer <= 0f ) {

				EnemyManager.instance.numEnemies--;
				Destroy(gameObject);
			}

		} else if (hit == true) {

			hitTimer -= Time.deltaTime;

			if (hitTimer <= 0f) {

				anim.ResetTrigger(damageHash);
				hit = false;
			}
		}
	}

	public void TakeDamage(float damage) {

		hit = true;

		health -= damage;

		if (health <= 0f)
		{

			if (dead != true) {

				deathSound.Play();
				Die();
			}

		} else {

			anim.SetTrigger(damageHash);

		}
	}

	void Die()
	{

		anim.SetTrigger(dieHash);
		dead = true;
	}
}
