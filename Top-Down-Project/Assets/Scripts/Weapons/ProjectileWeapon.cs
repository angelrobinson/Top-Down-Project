using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ProjectileWeapon : Weapon
{    

    [Header("Projectile Settings")]
    [SerializeField] Projectile bulletPrefab;
    [SerializeField] Transform barrel;
    [SerializeField] int damage;
    [SerializeField] float bulletVelocity;
    [SerializeField] int shotsPerMinute;
    [SerializeField] int spread = 1;

    //timer settings
    float readyToFire;
    float timeBetweenFire;
    public bool canShoot = true;

    private void OnEnable()
    {
        //divide the amount of seconds in a minute by the amount of shots per Minute
        //to get how much time is between each shot
        timeBetweenFire = 60f / shotsPerMinute;
    }

    
    private void FixedUpdate()
    {
        if (readyToFire <= 0)
        {
            canShoot = true;
        }
        else
        {
            readyToFire -= Time.deltaTime;
        }

        if (canShoot)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                //Pull the trigger to instantiate multiple shots
                for (int i = 0; i < spread; i++)
                {
                    PullTrigger();
                }
                
            }
        }        

    }

    public override void PullTrigger()
    {        
        Projectile bullet = Instantiate(bulletPrefab, barrel.position, barrel.rotation * Quaternion.Euler(Random.onUnitSphere * spread)) as Projectile;
        bullet.Damage = damage;
        bullet.rb.AddRelativeForce(Vector3.forward * bulletVelocity, ForceMode.VelocityChange);
        bullet.gameObject.layer = gameObject.layer;

        //reset the canshoot bool to false
        canShoot = false;
        //reset the timer
        readyToFire = timeBetweenFire;
    }
}


