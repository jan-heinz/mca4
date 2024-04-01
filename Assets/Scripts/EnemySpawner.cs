using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject bigEnemyPrefab;
    public float spawnTime = 3;
    public float xMin = -25;
    public float xMax = 25;
    public float yMin = 8;
    public float yMax = 25;
    public float zMin = -25;
    public float zMax = -25;

    int enemyCount = 0; // internal tracker
    
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("spawnEnemies", spawnTime, 3);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void spawnEnemies() {
        Vector3 enemyPosition;
        enemyPosition.x = Random.Range(xMin, xMax);
        enemyPosition.y = Random.Range(yMin, yMax);
        enemyPosition.z = Random.Range(zMin, zMax);

        if ((enemyCount + 1) % 4 == 0) { // every 4th enemy spawned will be big enemy
            GameObject bigSpawnedEnemy = Instantiate(bigEnemyPrefab, enemyPosition, transform.rotation) as GameObject;
            bigSpawnedEnemy.transform.parent = gameObject.transform;
        }
        else {
            GameObject spawnedEnemy = Instantiate(enemyPrefab, enemyPosition, transform.rotation) as GameObject;
            spawnedEnemy.transform.parent = gameObject.transform;
        }

        enemyCount++;


    }
}
