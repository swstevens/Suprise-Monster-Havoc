using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gen : MonoBehaviour
{
    public GameObject floor;
    public GameObject door;
    // Start is called before the first frame update
    void Start()
    {
        spawned.Add(new Vector2Int());
        occupied.Add(new Vector2Int());
        longest = UUID;
        connections.Add(new Vector2Int(), new HashSet<Vector2Int>());
        considered.Enqueue(new Vector2Int(0, 0));
        tiles.Add(new Vector2Int(), UUID++);
        Generate();        
    }





    static int UUID = 0;
    int longest = 0;
    public Dictionary<Vector2Int, int> tiles = new Dictionary<Vector2Int, int>();
    public Dictionary<Vector2Int, HashSet<Vector2Int>> connections = new Dictionary<Vector2Int, HashSet<Vector2Int>>();
    public HashSet<Vector2Int> spawned = new HashSet<Vector2Int>();
    public HashSet<Vector2Int> occupied = new HashSet<Vector2Int>();
    public Queue<Vector2Int> considered = new Queue<Vector2Int>();

    public int searchTime = 2;
    public int spreadTime = 5;
    public float giveUpChance = 0.9f;
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
            GameObject g = Instantiate(floor);
            g.transform.position = new Vector3(5 * s.x, -.5f, 5 * s.y);
        }

        HashSet<Vector2Int> alreadyConsidered = new HashSet<Vector2Int>();
        // Now for the doors, uses "generate a door is nearby"
        foreach (Vector2Int s in spawned)
        {
            dir[] d = new dir[] { dir.Left, dir.Up, dir.Down, dir.Right };
            foreach (dir dd in d)
            {
                if(IsThere(s, dd) && !alreadyConsidered.Contains(add(s,dd)) && spawned.Contains(add(s,dd)))
                {    
                    GameObject g = Instantiate(door);
                    g.transform.position = new Vector3(5*s.x + 2.5f*(add(s,dd)-s).x, .5f, 5*s.y + 2.5f * (add(s, dd) - s).y);
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

