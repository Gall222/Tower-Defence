using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] Transform objectToPan;
    [SerializeField] int attackRange = 30;
    [SerializeField] ParticleSystem projectile;

    Transform targetEnemy;
    public Waypoint baseWaypoint;
    // Update is called once per frame
    void Update()
    {
        SetTargetEnemy();
        if(targetEnemy)
            Fire();
        else
            Shoot(false);
    }

    private void SetTargetEnemy()
    {
        var sceneEnemies = FindObjectsOfType<EnemyDamage>();
        if (sceneEnemies.Length == 0) { return; }
        Transform closestEnemy = sceneEnemies[0].transform;
        foreach (var enemy in sceneEnemies)
        {
            closestEnemy = GetClosest(closestEnemy, enemy.transform);
        }
        targetEnemy = closestEnemy;
    }

    private Transform GetClosest(Transform transformA, Transform transformB)
    {
        var distToA = Vector3.Distance(transform.position, transformA.position);
        var distToB = Vector3.Distance(transform.position, transformB.position);
        if (distToA < distToB)
        {
            return transformA;
        }
        else
            return transformB;

    }

    private void Fire()
    {
        float distanceToEnemy = Vector3.Distance(targetEnemy.transform.position, gameObject.transform.position);
        print(distanceToEnemy);
        if (distanceToEnemy <= attackRange)
        {
            LookingAtTheEnemy();
            Shoot(true);
        }else
            Shoot(false);
    }

    private void Shoot(bool isActive)
    {
        var emissionModule = projectile.emission;
        emissionModule.enabled = isActive;
    }

    private void LookingAtTheEnemy()
    {
        objectToPan.LookAt(targetEnemy);
    }
}
