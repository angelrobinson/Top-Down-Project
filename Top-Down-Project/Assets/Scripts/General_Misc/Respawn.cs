﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    [Tooltip("How long do you want to wait until respawn")]
    [SerializeField] float respawnTime;
    [Tooltip("Object(s) to respawn")]
    [SerializeField] GameObject[] spawnObj;
    float timeLeft;
    int spawnIndex = 0;
    [SerializeField] bool randomSpawn;
    System.Random ran;


    // Start is called before the first frame update
    void Start()
    {
        //set default respawn time
        if (respawnTime == 0)
        {
            respawnTime = 5;
        }

        //set actual timer variable
        timeLeft = respawnTime;

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
        Instantiate(spawnObj[spawnIndex], transform.position, Quaternion.identity, transform);
    }

    private void FixedUpdate()
    {
        if (transform.childCount == 0)
        {
            //decrease time left until it hits zero
            if (timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
            }
            else
            {
                //if the spawn index is greater than the length of the array, then reset to 0
                if (spawnIndex >= spawnObj.Length || spawnIndex == 0 )
                {
                    spawnIndex = 0;
                }
                else
                {
                    //increase spawn index to the next
                    ++spawnIndex;
                }

                //if random select a new index to spawn
                if (randomSpawn)
                {
                    spawnIndex = ran.Next(0, spawnObj.Length);
                    Debug.Log("Random Spawn Index: " + spawnIndex);
                }

                //instantiate next object
                Instantiate(spawnObj[spawnIndex], transform.position, Quaternion.identity, transform);
            }
        }
        else
        {
            //reset respawn timer
            timeLeft = respawnTime;
        }
    }

    
}
