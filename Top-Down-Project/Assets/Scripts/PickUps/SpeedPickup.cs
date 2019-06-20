using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPickup : Pickup
{
    [SerializeField] float speed;
    /// <summary>
    /// this method is an override of what the base class has for this method.  
    /// It starts off with the specific things I want this pickup to do and then calls on the base pickup to destory the object
    /// </summary>
    /// <param name="player">Character object that </param>
    protected override void OnPickUp(Player player)
    {
        player.Speed += speed;
        base.OnPickUp(player);
    }
    
    
}
