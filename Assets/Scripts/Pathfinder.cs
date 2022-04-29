using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    [SerializeField] Waypoint startWaypoint, endWaypoint;

    Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();
    Queue<Waypoint> queue = new Queue<Waypoint>();

    bool isRunning = true;
    Waypoint searchCenter;
    List<Waypoint> path = new List<Waypoint>();
    public List<Waypoint> GetPath()
    {
        if (path.Count <=0)
        {
            CalculatePath();
        }

        return path;
    }

    private void CalculatePath()
    {
        LoadBlocks();
        BreadthFirstSearch();
        CreatePath();
    }

    Vector2Int[] directions =
    {
        Vector2Int.up,
        Vector2Int.right,
        Vector2Int.down,
        Vector2Int.left,
    };


    // Start is called before the first frame update
    void Start()
    {
   
    }

    private void CreatePath()
    {
        SetAsPath(endWaypoint);
        
        Waypoint previous = endWaypoint.exploredFrom;

        while(previous != startWaypoint)
        {
            SetAsPath(previous);
            previous = previous.exploredFrom;
        }
        SetAsPath(startWaypoint);
        path.Reverse();
    }
    private void SetAsPath(Waypoint waypoint)
    {
        path.Add(waypoint);
        endWaypoint.isPlaceable = false;
    }

    private void BreadthFirstSearch()
    {
        queue.Enqueue(startWaypoint);

        while (queue.Count > 0 && isRunning)
        {
            searchCenter = queue.Dequeue();
            searchCenter.isExplored = true;
            
            HaltIfEndFound();
            ExploreNeighbours();
        }
    }

    private void HaltIfEndFound()
    {
        if(searchCenter == endWaypoint)
        {
            isRunning = false;
        }
    }

    private void ExploreNeighbours()
    {
        if (!isRunning) { return; } 
        foreach (var direction in directions)
        {
            Vector2Int explorationCoord = searchCenter.GetGridPos() + direction;
            //print("Direction: " + explorationCoord);
            try
            {
                QueueNewNeighbours(explorationCoord);
                
            }
            catch
            {
                //print("!deleted " + explorationCoord);
            }
                
        }   
    }

    private void QueueNewNeighbours(Vector2Int explorationCoord)
    {
        var neighbour = grid[explorationCoord];
        if (neighbour.isExplored || queue.Contains(neighbour))
        {
            //print("explored " + neighbour);
        }
        else
        {
            queue.Enqueue(neighbour);
            neighbour.exploredFrom = searchCenter;
        }


    }


    private void LoadBlocks()
    {
        var waypoints = FindObjectsOfType<Waypoint>();
        foreach (var waypoint in waypoints)
        {
            var gridPos = waypoint.GetGridPos();
            
            if (grid.ContainsKey(gridPos))
            {
                Debug.LogWarning("Skipping block " + waypoint);
            }
            else
            {
                grid.Add(gridPos, waypoint);
            }
                
        }
    }


}
