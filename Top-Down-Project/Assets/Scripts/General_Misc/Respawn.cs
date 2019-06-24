using System.Collections;
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


    // Start is called before the first frame update
    void Start()
    {
        if (respawnTime == 0)
        {
            respawnTime = 5;
        }
        timeLeft = respawnTime;

        if (spawnObj.Length == 0 || spawnObj[0] == null)
        {
            throw new System.NullReferenceException("Spawn Objects were not set in the inspector");
        }

        Spawn(spawnObj[spawnIndex]);
    }

    private void FixedUpdate()
    {
        if (transform.childCount == 0)
        {
            if (timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
            }
            else
            {
                if (spawnIndex >= spawnObj.Length)
                {
                    spawnIndex = 0;
                }
                else
                {
                    ++spawnIndex;
                }

                Spawn(spawnObj[spawnIndex]);
            }
        }
        else
        {
            timeLeft = respawnTime;
        }
    }
    protected void Spawn(GameObject go)
    {
        Instantiate(go, transform.position, Quaternion.identity, transform);
    }
}
