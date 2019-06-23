using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int Damage { get; set; }
    [HideInInspector] public Rigidbody rb;
    [SerializeField] float lifespan;
    [Tooltip("In meters")]
    [SerializeField] float maxDistance;
    Vector3 startPostion;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        //set start position of where the projectile is instantiated
        startPostion = transform.position;

        //destroy the object after set amount of seconds defined in lifespan variable
        Destroy(gameObject, lifespan);
    }

    private void FixedUpdate()
    {
        //check if the bullet gets past the maxDistance from startPosition
        if (Vector3.Distance(transform.position, startPostion) > maxDistance)
        {
            //out of range and need to destroy the object
            Destroy(gameObject);
        }
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
