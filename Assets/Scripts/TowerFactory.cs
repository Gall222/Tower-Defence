using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFactory : MonoBehaviour
{
    [SerializeField] Tower towerPrefab;
    [SerializeField] int towerLimit = 5;
    [SerializeField] GameObject TowerParent;

    Queue<Tower> towers = new Queue<Tower>();

    // Start is called before the first frame update
    public void AddTower(Waypoint waypoint)
    {
        //var towers = FindObjectsOfType<Tower>();
        int towersNum = towers.Count;

        if (towersNum < towerLimit)
        {
            CreateNewTower(waypoint);
        }
        else
        {
            MoveExistingTower(waypoint);
        }
        
    }

    private void MoveExistingTower(Waypoint waypoint)
    {
        var oldTower = towers.Dequeue();
        oldTower.baseWaypoint.isPlaceable = true;
        oldTower.baseWaypoint = waypoint;
        oldTower.baseWaypoint.isPlaceable = false;
        oldTower.transform.position = waypoint.transform.position;
        towers.Enqueue(oldTower);
    }

    private void CreateNewTower(Waypoint waypoint)
    {
        var newTower = Instantiate(towerPrefab, waypoint.transform.position, Quaternion.identity);
        newTower.transform.parent = TowerParent.transform;
        newTower.baseWaypoint = waypoint;
        newTower.baseWaypoint.isPlaceable = false;
        towers.Enqueue(newTower);
        //towersNum++;
    }
}
