using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Pick up a weapon and switch to the new one
/// OR....pick up a weaon and add it to inventory. 
/// If doing inventory, will need a notice to the player on UI on how to switch weapons
/// </summary>
public class WeaponPickup : Pickup
{
    [SerializeField] GameObject weapon;


    private void Start()
    {
        Instantiate(weapon, gameObject.transform.position, gameObject.transform.rotation, gameObject.transform);
    }
    /// <summary>
    /// this method is an override of what the base class has for this method.  
    /// It starts off with the specific things I want this pickup to do and then calls on the base pickup to destory the object
    /// </summary>
    /// <param name="player"></param>
    protected override void OnPickUp(Player player)
    {
        //instantiate in the weapon placement on player
        //if there is already a weapon equipped, swap them out (Or, have an inventory and a swap button)
        base.OnPickUp(player);
    }
}
