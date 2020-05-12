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
    private int spawned = 0;
    // this number needs to stay the same across all roomspawners
    public int roomLimit = 10;
    private int i;

    void Start()
    {
        //Time.timeScale = 1f;
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        Invoke("Spawn", 0.1f);
        i = 1;
    }
    
    void Spawn()
    {
        // 0 - unspawned
        // 1 - room with possible collision
        // 2 - fully formed room, no possible ways to build upon

        // eventually unique tags for each direction to determine what kind of room can be spawned will be implemented (hopefully by beta)

        // spawn a room with any number of directions if the room limit has not been reached
        if ((spawned == 0) && (i < roomLimit))
        {
            if (openingDirection == 1)
            {
                    // spawn a room with a bottom door
                    rand = Random.Range(0, templates.bottomRooms.Length);
                    Instantiate(templates.bottomRooms[rand], transform.position, templates.bottomRooms[rand].transform.rotation);
                    i++;
            }
            else if (openingDirection == 2)
            {
                    // spawn a room with a top door
                    rand = Random.Range(0, templates.topRooms.Length);
                    Instantiate(templates.topRooms[rand], transform.position, templates.topRooms[rand].transform.rotation);
                    i++;
            }
            else if (openingDirection == 3)
            {
                    // spawn a room with a left door
                    rand = Random.Range(0, templates.leftRooms.Length);
                    Instantiate(templates.leftRooms[rand], transform.position, templates.leftRooms[rand].transform.rotation);
                    i++;
            }
            else if (openingDirection == 4)
            {
                    // spawn a room with a right door
                    rand = Random.Range(0, templates.rightRooms.Length);
                    Instantiate(templates.rightRooms[rand], transform.position, templates.rightRooms[rand].transform.rotation);
                    i++;
            } 
            spawned = 2;
        }
        else if (spawned == 0 && i >= roomLimit)
        {
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
                // Instantiate(templates.closedRoom, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
            spawned = 2;
        }
    }

}


