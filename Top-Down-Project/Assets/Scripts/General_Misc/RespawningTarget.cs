using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawningTarget : MonoBehaviour
{
    [Tooltip("How long do you want to wait until target respawns")]
    [SerializeField] float respawnTime;
    float timeLeft;
    ObjectHealth targetHealth;
    [SerializeField] GameObject targetObj;
    // Start is called before the first frame update
    void Start()
    {
        timeLeft = respawnTime;

        if (targetObj == null)
        {
            targetObj = gameObject.transform.Find("Target").gameObject;
        }

        targetHealth = targetObj.GetComponent<ObjectHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("target health " + targetHealth.Health);
        if (targetHealth.Health <= 0)
        {
            targetObj.SetActive(false);
        }

        if (!targetObj.activeInHierarchy)
        {
            if (timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
            }
            else
            {
                targetObj.SetActive(true);
            }
        }
        else
        {
            
            timeLeft = respawnTime;
        }
    }
}
