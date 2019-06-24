using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : Pickup
{
    public int healAmount;
    /// <summary>
    /// this method is an override of what the base class has for this method.  
    /// It starts off with the specific things I want this pickup to do and then calls on the base pickup to destory the object
    /// </summary>
    /// <param name="player"></param>
    protected override void OnPickUp(Player player)
    {
        player.MyHealth.Heal(healAmount);
        base.OnPickUp(player);
    }
}
