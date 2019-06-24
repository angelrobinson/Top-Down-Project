using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ObjectHealth))]
public class Target : MonoBehaviour
{
    ObjectHealth targetHealth;

    // Start is called before the first frame update
    void Start()
    {
        targetHealth = GetComponent<ObjectHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (targetHealth.Health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
