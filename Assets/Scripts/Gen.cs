using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gen : MonoBehaviour
{
    public GameObject door;
    public GameObject enemyModel;
    public float roomSize; 
    private int numHeals;
    public int maxHeals = 2;
    private int rand;
    private bool eSpawned;
    // Start is called before the first frame update
    void Start()
    {
    	numHeals = 0;
    	eSpawned = false;
        spawned.Add(new Vector2Int());
        occupied.Add(new Vector2Int());
        longest = UUID;
        connections.Add(new Vector2Int(), new HashSet<Vector2Int>());
        considered.Enqueue(new Vector2Int(0, 0));
        tiles.Add(new Vector2Int(), UUID++);
        Generate();        
    }





    static int UUID = 0; // for floor tiles
    int longest = 0;
    public Dictionary<Vector2Int, int> tiles = new Dictionary<Vector2Int, int>();
    public Dictionary<Vector2Int, HashSet<Vector2Int>> connections = new Dictionary<Vector2Int, HashSet<Vector2Int>>();
    public HashSet<Vector2Int> spawned = new HashSet<Vector2Int>();
    public HashSet<Vector2Int> occupied = new HashSet<Vector2Int>();
    public Queue<Vector2Int> considered = new Queue<Vector2Int>();

    public int searchTime = 2;
    public int spreadTime = 5;
    public float giveUpChance = 0.9f;
    public float healChance = .08f;
    public float ElevatorChance = .08f;
    public float enemyChance = .1f;
    public bool searchMode = false;
    public bool CM = false;
    public int credits = 20;

    public int credSpent = 0;
    public int MAX_LOOPS = 500;
    public int corLen = 4;
    public int CCL = 4;

    private void Generate()
    {
        Vector2Int override1 = default(Vector2Int);
        bool useOver = false;
        bool cooldown = false;
        dir pcd = dir.Down;
        while(credits > 0 && MAX_LOOPS > 0)
        {
            MAX_LOOPS--;
            Vector2Int cur = default(Vector2Int);
            if (!useOver)
            {
                cur = considered.Dequeue();
            }
            else
            {
                useOver = false;
                cur = override1;
            }
            if(credSpent == searchTime && searchMode)
            {
                searchMode = !searchMode;
                credSpent = 0;
            }else if(credSpent == spreadTime && !searchMode)
            {
                searchMode = !searchMode;
                CM = !CM;
                cooldown = true;
                credSpent = 0;
            }

            Vector2Int suc = default(Vector2Int);
            dir[] d = { };
            if (!cooldown && CM && searchMode)
            {
                d = new dir[]{ pcd};
            }
            else {
                d = new dir[] { dir.Left, dir.Up, dir.Down, dir.Right };
            } 
            // Knuth shuffle algorithm :: courtesy of Wikipedia :)
            for (int t = 0; t < d.Length; t++)
            {
                dir tmp = d[t];
                int r = Random.Range(t, d.Length);
                d[t] = d[r];
                d[r] = tmp;
            }
            bool givenUp = false;
            bool success = false;
            // Add a shuffle :)
            foreach(dir dd in d)
            {
                if(IsThere(cur, dd))
                {
                    if(Random.Range(0,1f) <= giveUpChance)
                    {
                        // Keep going
                        //pass
                    }
                    else
                    {
                        // Give up...
                        givenUp = true;
                        break;
                    }
                }
                else
                {
                    // Nope, generate :P
                    Vector2Int dda = add(cur, dd);
                    spawned.Add(dda);
                    occupied.Add(dda);
                    longest = UUID;
                    connections.Add(dda, new HashSet<Vector2Int>());
                    connections[cur].Add(dda);
                    connections[dda].Add(cur);
                    considered.Enqueue(dda);
                    tiles.Add(dda, UUID++);
                    success = true;
                    suc = dda;
                    credits--;
                    credSpent++;

                    if (CM && searchMode)
                    {
                        if (dd == dir.Down || dd == dir.Up)
                        {
                            occupied.Add(add(cur, dir.Left));
                            occupied.Add(add(cur, dir.Right));
                        }
                        else
                        {
                            occupied.Add(add(cur, dir.Up));
                            occupied.Add(add(cur, dir.Down));
                        }
                    }


                    if(CM && cooldown && searchMode)
                    {
                        cooldown = false;
                        pcd = dd;
                        corLen = CCL - 1;
                    }
                    else if(CM && searchMode)
                    {
                        corLen--;
                        
                        if (corLen == 0)
                        {
                            CM = false;
                        }
                    }

                    break;
                }
            }


            if (givenUp)
            {
                // Retry :D
                considered.Enqueue(cur);
                continue;
            }
            else if (success)
            {
                if (searchMode)
                {
                    override1 =suc;
                    useOver = true;
                    considered.Enqueue(cur);
                }
                else
                {
                    considered.Enqueue(cur);
                }
            }
            else
            {
                continue;
            }
        }

        // Above generates rooms locs, below generates rooms

        foreach(Vector2Int s in spawned)
        {
        	rand = Random.Range(0, TileManager.instance.floors.Length);
            GameObject g = Instantiate(TileManager.instance.floors[rand]);
            g.transform.position = new Vector3(roomSize * s.x, 0f, roomSize * s.y);
        }
        //Vector2Int origin = new Vector2Int(0,0);
        foreach(Vector2Int s in spawned)
        {
    //     	check if there are any nearby occupied? spaces
        	if (Random.Range(0,1f) <= ElevatorChance && eSpawned == false)
        	{
        		if (Mathf.Abs(s.x) > 1 && Mathf.Abs(s.y) > 1) // don't spawn on top of player
        		{
	            	GameObject g = Instantiate(TileManager.instance.elevator);
					g.transform.position = new Vector3(roomSize * s.x, 0f, roomSize * s.y);
					eSpawned = true;
				}
        	}
        	else if (Random.Range(0,1f) <= healChance && numHeals < maxHeals)
        	{
	            GameObject g = Instantiate(TileManager.instance.healStation);
				g.transform.position = new Vector3(roomSize * s.x, 0f, roomSize * s.y);
				numHeals++;
        	}
        	else if (Random.Range(0,1f) <= enemyChance)
        	{
        		if (Mathf.Abs(s.x)>= 4 && Mathf.Abs(s.y) >= 4)
        		{
        			// add if distance from 0,0,0 is larger than 10? idk something
        			GameObject g = Instantiate(TileManager.instance.enemy);
            		g.transform.position = new Vector3(roomSize * s.x, 0f, roomSize * s.y);
        		}
        	}
        }
        // there's a chance that the player low rolls and never gets an elevator with this system
        // will need to add a safety system possibly (only concern is it might spawn on top of an enemy though)

        HashSet<Vector2Int> alreadyConsidered = new HashSet<Vector2Int>();
        // Now for the doors and walls, uses "generate a door is nearby"
        foreach (Vector2Int s in spawned)
        {
            dir[] d = new dir[] { dir.Left, dir.Up, dir.Down, dir.Right };
            foreach (dir dd in d)
            {
                // spawn doors
                if(IsThere(s, dd) && !alreadyConsidered.Contains(add(s,dd)) && spawned.Contains(add(s,dd)))
                {    
                    // GameObject x = Instantiate(TileManager.instance.enemy);
                    // x.transform.position = new Vector3(roomSize * s.x, 0f, roomSize * s.y);
                    // for testing to see if I can identify corridors
                    //GameObject g = Instantiate(door);
                    //.transform.position = new Vector3(roomSize*s.x + roomSize/2*(add(s,dd)-s).x, 0f, roomSize*s.y + roomSize/2 * (add(s, dd) - s).y);
                }
                // spawn walls
                if(!alreadyConsidered.Contains(add(s,dd)) && !spawned.Contains(add(s,dd)))
                {
                	//add extra if case here for heal station spawning (if chance met and number of spawned elevator < limit)
                    if (dd == dir.Down || dd == dir.Up)
                    {
                    	rand = Random.Range(0, TileManager.instance.horizontalWalls.Length);
                        GameObject g = Instantiate(TileManager.instance.horizontalWalls[rand]);
                        g.transform.position = new Vector3(roomSize*s.x + roomSize/2*(add(s,dd)-s).x, 0f, roomSize*s.y + roomSize/2 * (add(s, dd) - s).y);
                    }
                    else 
                    {
                    	rand = Random.Range(0, TileManager.instance.verticalWalls.Length);
                        GameObject g = Instantiate(TileManager.instance.verticalWalls[rand]);
                        g.transform.position = new Vector3(roomSize*s.x + roomSize/2*(add(s,dd)-s).x, 0f, roomSize*s.y + roomSize/2 * (add(s, dd) - s).y);
                    }
                }
                alreadyConsidered.Add(s);


            }
        }
    }

    public enum dir {Left, Right, Up, Down };
    private bool IsThere(Vector2Int a, dir d)
    {
        switch (d)
        {
            case dir.Left:
                return occupied.Contains(a + new Vector2Int(-1, 0));
            case dir.Right:
                return occupied.Contains(a + new Vector2Int(1, 0));
            case dir.Up:
                return occupied.Contains(a + new Vector2Int(0, -1));
            case dir.Down:
                return occupied.Contains(a + new Vector2Int(0, 1));
            default:
                return false;
        }
    }
    private Vector2Int add(Vector2Int a, dir d)
    {
        switch (d)
        {
            case dir.Left:
                return (a + new Vector2Int(-1, 0));
            case dir.Right:
                return (a + new Vector2Int(1, 0));
            case dir.Up:
                return (a + new Vector2Int(0, -1));
            case dir.Down:
                return a + new Vector2Int(0, 1);
            default:
                return a;
        }
    }

}

