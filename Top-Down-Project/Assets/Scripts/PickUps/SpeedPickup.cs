  using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPickup : Pickup
{
    [Tooltip("How much do you want the speed to increase")]
    [SerializeField] float speed;
    [Tooltip("if you want the buff to be permanent, put in zero or any negative number")]
    [SerializeField] float timeLimit;
    
    /// <summary>
    /// this method is an override of what the base class has for this method.  
    /// It starts off with the specific things I want this pickup to do and then calls on the base pickup to destory the object
    /// </summary>
    /// <param name="player">Character object that </param>
    protected override void OnPickUp(Player player)
    {
        PlayerController pc = player.GetComponent<PlayerController>();

        Debug.Log("Speed OnPickup Triggered");
        player.Speed += speed;
        pc.isBuffed = true;
        pc.buffType = PlayerController.BuffType.Speed;
        pc.buffTime = timeLimit;
        Debug.Log("Player speed is: " + player.Speed);
        player.Stamina += speed;
        base.OnPickUp(player);
    }
    
    
}
