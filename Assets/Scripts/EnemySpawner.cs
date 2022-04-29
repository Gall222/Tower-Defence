using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Range(1,5)] [SerializeField] float secondsBetweenSpawns = 3f;
    [SerializeField] EnemyMovement enemyPrefab;
    [SerializeField] int enemyCount = 3;
    [SerializeField] AudioClip spawnSFX;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawningEnemy());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator SpawningEnemy()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            var newEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            newEnemy.transform.parent = transform;

            GetComponent<AudioSource>().PlayOneShot(spawnSFX);
            yield return new WaitForSeconds(secondsBetweenSpawns);
        }

    }
}
