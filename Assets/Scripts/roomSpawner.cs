using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roomSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public int openingDirection;
    // 1 -> need bottom door
    // 2 -> need top door
    // 3 -> need left door
    // 4 -> need right door

    private RoomTemplates templates;
    private int rand;
    public int spawned = 0;
    // this number needs to stay the same across all roomspawners
    public int roomLimit = 10;
    public static int i;
    public float waitTime = 5f;

    void Start()
    {
        //Time.timeScale = 1f;
        // Destroying all nodes after five seconds appears to be causing problems (not every node has bee fully spawned before nodes are deleted)
        //Destroy(gameObject, waitTime);
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        Invoke("Spawn", 1f);
    }
    
    void Spawn()
    {
        // 0 - unspawned
        // 1 - room with possible collision
        // 2 - fully formed room, no possible ways to build upon

        // eventually unique tags for each direction to determine what kind of room can be spawned will be implemented (hopefully by beta)

        // spawn a room with any number of directions if the room limit has not been reached
        if ((spawned == 0) && (MapManager.instance.i < MapManager.instance.mapLimit))
        {
            if (openingDirection == 1)
            {
                    // spawn a room with a bottom door
                    rand = Random.Range(0, templates.bottomRooms.Length);
                    Instantiate(templates.bottomRooms[rand], transform.position, templates.bottomRooms[rand].transform.rotation);
                    MapManager.instance.i++;
                    Debug.Log("Bottom Door Spawned (0 & below limit) 0->2");
            }
            else if (openingDirection == 2)
            {
                    // spawn a room with a top door
                    rand = Random.Range(0, templates.topRooms.Length);
                    Instantiate(templates.topRooms[rand], transform.position, templates.topRooms[rand].transform.rotation);
                    MapManager.instance.i++;
                    Debug.Log("Top Door Spawned (0 & below limit) 0->2");
            }
            else if (openingDirection == 3)
            {
                    // spawn a room with a left door
                    rand = Random.Range(0, templates.leftRooms.Length);
                    Instantiate(templates.leftRooms[rand], transform.position, templates.leftRooms[rand].transform.rotation);
                    MapManager.instance.i++;
                    Debug.Log("Left Door Spawned (0 & below limit) 0->2");
            }
            else if (openingDirection == 4)
            {
                    // spawn a room with a right door
                    rand = Random.Range(0, templates.rightRooms.Length);
                    Instantiate(templates.rightRooms[rand], transform.position, templates.rightRooms[rand].transform.rotation);
                    MapManager.instance.i++;
                    Debug.Log("Right Door Spawned (0 & below limit) 0->2");
            } 
            spawned = 2;
        }
        else if (spawned == 0 && MapManager.instance.i >= MapManager.instance.mapLimit)
        {
        	// mark all leftover doors to nowhere as needing an end room
            spawned = 1;
            Debug.Log("Leftover room set to 2");
        }
        // if spawn == 1, then spawn an end room
        if (spawned == 1)
        {
            if (openingDirection == 1)
            {
                // spawn a bottom end room
                rand = Random.Range(0, templates.bottomEndRooms.Length);
                Instantiate(templates.bottomEndRooms[rand], transform.position, templates.bottomEndRooms[rand].transform.rotation);
                Debug.Log("Bottom End Spawned 1->2");
            }
            else if (openingDirection == 2)
            {
                // spawn a top end room
                rand = Random.Range(0, templates.topEndRooms.Length);
                Instantiate(templates.topEndRooms[rand], transform.position, templates.topEndRooms[rand].transform.rotation);
                Debug.Log("Top End Spawned 1->2");
            }
            else if (openingDirection == 3)
            {
                // spawn a left end room
                rand = Random.Range(0, templates.leftEndRooms.Length);
                Instantiate(templates.leftEndRooms[rand], transform.position, templates.leftEndRooms[rand].transform.rotation);
                Debug.Log("Left End Spawned 1->2");
            }
            else if (openingDirection == 4)
            {
                // spawn a right end room
                rand = Random.Range(0, templates.rightEndRooms.Length);
                Instantiate(templates.rightEndRooms[rand], transform.position, templates.rightEndRooms[rand].transform.rotation);
                Debug.Log("Right End Spawned 1->2");
            } 
            spawned = 2;
        }
    }

    void OnTriggerEnter(Collider other)
    {
    	if (other.CompareTag("Destroyer"))
    	{
    		spawned = 2;
    	}

        if (other.CompareTag("NoRoom"))
        {
            // spawn a wall object instead of a room
            //Instatiate(templates.closedRoom, transform.position, Quaternion.identity);
            spawned = 1;
        }
        
        if (other.CompareTag("SpawnPoint"))
        {
        	// they need to be 
            if (other.GetComponent<roomSpawner>().spawned == 0 && spawned == 0)
            {
            	// spawn a wall
                if (openingDirection == 1)
            	{
                	// spawn a bottom end room
                	rand = Random.Range(0, templates.bottomEndRooms.Length);
                	Instantiate(templates.bottomEndRooms[0], transform.position, templates.bottomEndRooms[rand].transform.rotation);
                	Debug.Log("Bottom End Spawned(collision encountered) 0->2");
            	}
            	else if (openingDirection == 2)
            	{
                	// spawn a top end room
                	rand = Random.Range(0, templates.topEndRooms.Length);
                	Instantiate(templates.topEndRooms[0], transform.position, templates.topEndRooms[rand].transform.rotation);
                	Debug.Log("Top End Spawned(collision encountered) 0->2");
            	}
            	else if (openingDirection == 3)
            	{
                	// spawn a left end room
                	rand = Random.Range(0, templates.leftEndRooms.Length);
                	Instantiate(templates.leftEndRooms[0], transform.position, templates.leftEndRooms[rand].transform.rotation);
                	Debug.Log("Left End Spawned(collision encountered) 0->2");
            	}
            	else if (openingDirection == 4)
            	{
                	// spawn a right end room
                	rand = Random.Range(0, templates.rightEndRooms.Length);
                	Instantiate(templates.rightEndRooms[0], transform.position, templates.rightEndRooms[rand].transform.rotation);
                	Debug.Log("Right End Spawned(collision encountered) 0->2");
            	} 
                // Instantiate(templates.closedRoom, transform.position, Quaternion.identity);
                Destroy(gameObject);
                // putting this inside the loop solves another issue of doors to nowhere
                spawned = 2;
                // should solve the doors to nowhere that are right next to one another
                other.GetComponent<roomSpawner>().spawned = 1;
            }
            // This is causing errors 
            if ((other.GetComponent<roomSpawner>().spawned == 1 || other.GetComponent<roomSpawner>().spawned == 2) && spawned == 0)
            {
                spawned = 1;
            }
        }
    }

}


