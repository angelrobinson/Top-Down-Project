using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ProjectileWeapon : Weapon
{
    [Header("Projectile Settings")]
    [SerializeField] private Projectile bulletPrefab;
    [SerializeField] private Transform barrel;
    [SerializeField] private int damage;
    [SerializeField] private float bulletVelocity;
    [SerializeField] private int shotsPerMinute;
    int spread = 1;

    private float readyToFire;

    public bool IsFiring { get; private set; }
    public bool canshoot = true;

    private void OnEnable()
    {
        readyToFire = 60f / shotsPerMinute;
        
    }

    private void FixedUpdate()
    {
        

        if (Input.GetKeyUp(KeyCode.F))
        {
            PullTrigger();
        }
        else
        {
            ReleaseTrigger();
        }

        if (IsFiring)
        {
            Projectile bullet = Instantiate(bulletPrefab, barrel.position, barrel.rotation * Quaternion.Euler(Random.onUnitSphere * spread));
            bullet.Damage = damage;
            bullet.rb.AddRelativeForce(Vector3.forward * bulletVelocity, ForceMode.VelocityChange);
            bullet.gameObject.layer = gameObject.layer;
        }

        
    }

    public override void PullTrigger()
    {
        IsFiring = true;
        
    }

    public override void ReleaseTrigger()
    {
        IsFiring = false;
    }
}


/*
 * private void Awake()
{
    timeNextShotIsReady = Time.time;
} 

private void Update()
{
    if (triggerPulled)
    {
        while (Time.time > timeNextShotIsReady)
        {
            // Shoot
            timeNextShotIsReady += 60f / shotsPerMinute;
        }
    }
    else if (Time.time > timeNextShotIsReady)
    {
        timeNextShotIsReady = Time.time;
    }
}

    */