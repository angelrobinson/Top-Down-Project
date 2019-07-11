﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ProjectileWeapon : Weapon
{    

    [Header("Projectile Settings")]
    [Tooltip("List any bullets that the gun can use")]
    [SerializeField] Projectile[] bulletPrefab = default; 
    [Tooltip("List all barrels that the gun can shoot from")]
    [SerializeField] Transform[] barrel;
    [SerializeField] int damage;
    [SerializeField] float bulletVelocity;
    [SerializeField] int shotsPerMinute;
    [SerializeField] int spread = 1;


    [Header("Timer Settings")]
    float readyToFire;
    float timeBetweenFire;
    public bool canShoot = true;

    private void OnEnable()
    {
        //divide the amount of seconds in a minute by the amount of shots per Minute
        //to get how much time is between each shot
        timeBetweenFire = 60f / shotsPerMinute;

        //set defaults 
        if (MaxDistance <= 0)
        {
            MaxDistance = 25;
        }

        if (damage == 0)
        {
            damage = 10;
        }

        if (barrel.Length == 0)
        {
            barrel = new Transform[1];
        }

        if (bulletVelocity == 0)
        {
            bulletVelocity = 10;
        }

        if (shotsPerMinute == 0)
        {
            shotsPerMinute = 100;
        }

        Audio = GetComponent<AudioSource>();
    }

    
    private void FixedUpdate()
    {
        //check to see if you can shoot
        if (readyToFire <= 0)
        {
            canShoot = true;
        }
        else
        {
            //update timer for checking if ready to shoot
            readyToFire -= Time.deltaTime;
        }
    }

    public override void PullTrigger()
    {
        //create temp variable to be able to assign settings on the projectile
        Projectile bullet;

        //instantiate bullet
        for (int i = 0; i < barrel.Length; i++)
        {
            if (bulletPrefab.Length > 1)
            {
                bullet = Instantiate(bulletPrefab[i], barrel[i].position, barrel[i].rotation * Quaternion.Euler(Random.onUnitSphere * spread)) as Projectile;
            }
            else
            {
                bullet = Instantiate(bulletPrefab[0], barrel[i].position, barrel[i].rotation * Quaternion.Euler(Random.onUnitSphere * spread)) as Projectile;
            }

            //add damage, force and layer to the bullet
            bullet.Damage = damage;
            bullet.rb.AddRelativeForce(Vector3.forward * bulletVelocity, ForceMode.VelocityChange);
            bullet.gameObject.layer = gameObject.layer;

            //TODO: buck shot issue fix
        }

        if (Audio)
        {
            //play shot sound
            Audio.PlayOneShot(shotSound);
        }

        foreach (var item in MuzzleFlash)
        {
            item.Emit(1);
        }
        //if (MuzzleFlash)
        //{
        //    MuzzleFlash.Emit(1);
        //}
        //reset the canshoot bool to false
        canShoot = false;
        //reset the timer
        readyToFire = timeBetweenFire;
    }
}


