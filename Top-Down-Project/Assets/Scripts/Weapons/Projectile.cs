using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int Damage { get; set; }
    [HideInInspector] public Rigidbody rb;
    [SerializeField] private float lifespan;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        //destroy the object after set amount of seconds defined in lifespan variable
        Destroy(gameObject, lifespan);
    }

    private void OnCollisionEnter(Collision collision)
    {
        ObjectHealth health = collision.gameObject.GetComponent<ObjectHealth>();

        if (health)
        {
            health.TakeDamage(Damage);
        }

        Destroy(gameObject);
    }
}
