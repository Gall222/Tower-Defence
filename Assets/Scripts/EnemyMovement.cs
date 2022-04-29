using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float enemyStepPause = 2f;
    Pathfinder pathfinder;
    List<Waypoint> path;
    BaseHealth baseHealth;
    // Start is called before the first frame update
    void Start()
    {
        pathfinder = FindObjectOfType<Pathfinder>();
        baseHealth = FindObjectOfType<BaseHealth>();
        path = pathfinder.GetPath();
        StartCoroutine(FollowPath());
    }

    IEnumerator FollowPath()
    {
        foreach (var point in path)
        {
            transform.position = point.transform.position;
            yield return new WaitForSeconds(enemyStepPause);
        }
        baseHealth.GetHealth();
        GetComponent<EnemyDamage>().Death(true);
    }


}
