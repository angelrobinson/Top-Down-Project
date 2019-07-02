using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawning : MonoBehaviour
{
    [Tooltip("How long do you want to wait until respawn")]
    [SerializeField] float respawnTime;
    [Tooltip("Object(s) to respawn")]
    [SerializeField] GameObject[] spawnObj;
    int spawnIndex = 0;
    [SerializeField] bool randomSpawn;
    System.Random ran;

    [Header("EnemySettings")]
    [SerializeField] int maxEnemies;
    int enemySpawnCount;
    int activeEnemies;


    // Start is called before the first frame update
    void Start()
    {
        //set default respawn time
        if (respawnTime == 0)
        {
            respawnTime = 5;
        }


        //if there aren't any objects to spawn, this throws a Null Reference Exception
        if (spawnObj.Length == 0 || spawnObj[0] == null)
        {
            throw new System.NullReferenceException("Spawn Objects were not set in the inspector");
        }


        //if randome is selected then choose the random spawn index
        if (randomSpawn)
        {
            ran = new System.Random();
            spawnIndex = ran.Next(0, spawnObj.Length);
        }

        //spawn first object
        //Instantiate(spawnObj[spawnIndex], transform);

        InvokeRepeating("Spawn", respawnTime, respawnTime);
    }

    private void Update()
    {
        activeEnemies = transform.childCount;
    }
    

    void Spawn()
    {
       
        //Don't spawn if spawn count reaches max enemies, return
        if (activeEnemies >= maxEnemies)
        {
            return;
        }
        
        //spawn the enemy

        //if random select a new index to spawn
        if (randomSpawn)
        {
            spawnIndex = ran.Next(0, spawnObj.Length);
        }
        else
        {
            //increase spawn index to the next
            ++spawnIndex;

            //if the spawn index is greater than the length of the array, then reset to 0
            if (spawnIndex >= spawnObj.Length)
            {
                spawnIndex = 0;
            }
        }

        //instantiate next object
        Instantiate(spawnObj[spawnIndex], transform);

        //increase the enemy spawn count        
        enemySpawnCount++;

        if (enemySpawnCount >= maxEnemies)
        {
            CancelInvoke("Spawn");
        }
    }
}
