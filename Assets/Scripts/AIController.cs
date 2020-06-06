using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class AIController : MonoBehaviour {

    Transform target;
    //NavMeshAgent agent;

    public float MoveSpeed = 4f;
    public float MaxDist = 8f;
    public float distance;
    public float MinDist = 2f;
    private int damage;

    public AudioSource vocal;
    private float vocalTimer = 2.0f;
    //private bool moving;

    private bool hit;
    private bool dead;

    Animator anim;
    int moveHash = Animator.StringToHash ("Walk Forward");
    int attackHash = Animator.StringToHash ("Stab Attack");

    public Component damageCollider;

    void Start() 
    {
    	//agent = GetComponent<NavMeshAgent>();
    	target = PlayerManager.instance.player.transform;
    	EnemyManager.instance.numEnemies++;
    	damage = EnemyManager.instance.damage;
    	anim = GetComponent<Animator>();

    }
 
    void Update() 
    {

        dead = gameObject.GetComponent<Target>().dead;
        hit = gameObject.GetComponent<Target>().hit;

        //transform.LookAt(Player);
        distance = Vector3.Distance(target.position, transform.position);

        if (dead == true) {

            MoveSpeed = 0;
            Destroy(damageCollider);

        } else if (hit == true) {

            anim.ResetTrigger(moveHash);
            transform.position += transform.forward * 0 * Time.deltaTime;

        } else if (distance <= MinDist) {

 			anim.ResetTrigger(moveHash);

 			anim.SetTrigger(attackHash);

 			Vector3 direction = (target.position - transform.position).normalized;
 			Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
			transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 8f);

			// do an attack
 		} else if (distance <= MaxDist) {
 			//agent.SetDestination(target.position);
			// need to figure out how to make navmeshes

            if (dead != true) {

                vocalTimer -= Time.deltaTime;

                if (vocalTimer <= 1) {

                    vocal.Play();
                    vocalTimer = 2.0f;
                }
                
                anim.ResetTrigger(attackHash);

                anim.SetTrigger(moveHash);

                Vector3 direction = (target.position - transform.position).normalized;
                Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 8f);

                transform.position += transform.forward * MoveSpeed * Time.deltaTime;
            
            } 
 
        } else {

        	anim.ResetTrigger(moveHash);
        }
    }

    void onTriggerStay(Collider other)
    {
    	if (other.gameObject.CompareTag("Player"))
    	{
            if (dead != true) {

                PlayerManager.instance.hp -= 1;
                //Debug.Log(PlayerManager.instance.hp);
            } else {

                damage = 0;
            }
    	}
    }

    void OnDrawGizmosSelected()
    {
    	Gizmos.color = Color.red;
    	Gizmos.DrawWireSphere(transform.position, MaxDist);
    	Gizmos.DrawWireSphere(transform.position,MinDist);
    }
}
