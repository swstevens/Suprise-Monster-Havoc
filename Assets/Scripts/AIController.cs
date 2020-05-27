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
    //int MinDist = 5;

    void Start() 
    {
    	//agent = GetComponent<NavMeshAgent>();
    	target = PlayerManager.instance.player.transform;
    	EnemyManager.instance.numEnemies++;
    }
 
    void Update() 
    {
        //transform.LookAt(Player);
        distance = Vector3.Distance(target.position, transform.position);

 
        if (distance <= MaxDist) {
 			//agent.SetDestination(target.position);
			// need to figure out how to make navmeshes

			Vector3 direction = (target.position - transform.position).normalized;
			Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
			transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 8f);

            transform.position += transform.forward * MoveSpeed * Time.deltaTime;

        }
    }

    void OnDrawGizmosSelected()
    {
    	Gizmos.color = Color.red;
    	Gizmos.DrawWireSphere(transform.position, MaxDist);
    }
}
