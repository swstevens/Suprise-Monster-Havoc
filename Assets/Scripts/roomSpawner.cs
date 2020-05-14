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
        Destroy(gameObject, waitTime);
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        Invoke("Spawn", 0.1f);
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
            }
            else if (openingDirection == 2)
            {
                    // spawn a room with a top door
                    rand = Random.Range(0, templates.topRooms.Length);
                    Instantiate(templates.topRooms[rand], transform.position, templates.topRooms[rand].transform.rotation);
                    MapManager.instance.i++;
            }
            else if (openingDirection == 3)
            {
                    // spawn a room with a left door
                    rand = Random.Range(0, templates.leftRooms.Length);
                    Instantiate(templates.leftRooms[rand], transform.position, templates.leftRooms[rand].transform.rotation);
                    MapManager.instance.i++;
            }
            else if (openingDirection == 4)
            {
                    // spawn a room with a right door
                    rand = Random.Range(0, templates.rightRooms.Length);
                    Instantiate(templates.rightRooms[rand], transform.position, templates.rightRooms[rand].transform.rotation);
                    MapManager.instance.i++;
            } 
            spawned = 2;
        }
        else if (spawned == 0 && MapManager.instance.i >= MapManager.instance.mapLimit)
        {
        	// mark all leftover doors to nowhere as needing an end room
            spawned = 1;
        }
        // if spawn == 1, then spawn an end room
        if (spawned == 1)
        {
            if (openingDirection == 1)
            {
                // spawn a bottom end room
                rand = Random.Range(0, templates.bottomEndRooms.Length);
                Instantiate(templates.bottomEndRooms[rand], transform.position, templates.bottomEndRooms[rand].transform.rotation);
            }
            else if (openingDirection == 2)
            {
                // spawn a top end room
                rand = Random.Range(0, templates.topEndRooms.Length);
                Instantiate(templates.topEndRooms[rand], transform.position, templates.topEndRooms[rand].transform.rotation);
            }
            else if (openingDirection == 3)
            {
                // spawn a left end room
                rand = Random.Range(0, templates.leftEndRooms.Length);
                Instantiate(templates.leftEndRooms[rand], transform.position, templates.leftEndRooms[rand].transform.rotation);
            }
            else if (openingDirection == 4)
            {
                // spawn a right end room
                rand = Random.Range(0, templates.rightEndRooms.Length);
                Instantiate(templates.rightEndRooms[rand], transform.position, templates.rightEndRooms[rand].transform.rotation);
            } 
            spawned = 2;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NoRoom"))
        {
            // spawn a wall object instead of a room
            //Instatiate(templates.closedRoom, transform.position, Quaternion.identity);
            spawned = 1;
        }
        
        if (other.CompareTag("SpawnPoint"))
        {
            if (other.GetComponent<roomSpawner>().spawned == 0 && spawned == 0)
            {
            	// spawn a wall
                if (openingDirection == 1)
            	{
                	// spawn a bottom end room
                	rand = Random.Range(0, templates.bottomEndRooms.Length);
                	Instantiate(templates.bottomEndRooms[rand], transform.position, templates.bottomEndRooms[rand].transform.rotation);
            	}
            	else if (openingDirection == 2)
            	{
                	// spawn a top end room
                	rand = Random.Range(0, templates.topEndRooms.Length);
                	Instantiate(templates.topEndRooms[rand], transform.position, templates.topEndRooms[rand].transform.rotation);
            	}
            	else if (openingDirection == 3)
            	{
                	// spawn a left end room
                	rand = Random.Range(0, templates.leftEndRooms.Length);
                	Instantiate(templates.leftEndRooms[rand], transform.position, templates.leftEndRooms[rand].transform.rotation);
            	}
            	else if (openingDirection == 4)
            	{
                	// spawn a right end room
                	rand = Random.Range(0, templates.rightEndRooms.Length);
                	Instantiate(templates.rightEndRooms[rand], transform.position, templates.rightEndRooms[rand].transform.rotation);
            	} 
                // Instantiate(templates.closedRoom, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
            spawned = 2;
        }
    }

}


