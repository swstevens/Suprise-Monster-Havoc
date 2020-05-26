using System.Globalization;
using UnityEngine;
using UnityEngine.UI;


public class Gun : MonoBehaviour
{
	public float damage = 10f;
	public float range = 100f;

	public Camera fpsCam;

	public int bullets = 30;

	void Update()
	{
		if (Input.GetButtonDown("Fire1") && (bullets > 0))
		{
			Shoot();
		}

		if (Input.GetKeyDown(KeyCode.R))
		{
			Reload();
		}
	}

	void Shoot()
	{
		bullets -= 1;

		RaycastHit hit;
		if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
		{
			Debug.Log(hit.transform.name);

			Target target = hit.transform.GetComponent<Target>();
			if (target != null)
			{
				target.TakeDamage(damage);
			}
		}

	}

	void Reload()
	{
		bullets = 30;
	}

}