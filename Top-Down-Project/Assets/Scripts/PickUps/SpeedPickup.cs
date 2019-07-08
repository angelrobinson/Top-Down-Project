  using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPickup : Pickup
{
    [Tooltip("How much do you want the speed to increase")]
    [SerializeField] float speed;
    [Tooltip("if you want the buff to be permanent, put in any number less than or equal to  -1")]
    [SerializeField] float timeLimit;

    private void Awake()
    {
        if (speed == 0)
        {
            speed = 10;
        }

        if (timeLimit == 0)
        {
            timeLimit = 0;
        }
    }
    /// <summary>
    /// this method is an override of what the base class has for this method.  
    /// It starts off with the specific things I want this pickup to do and then calls on the base pickup to destory the object
    /// </summary>
    /// <param name="player">Character object that </param>
    protected override void OnPickUp(Player player)
    {
        PlayerController pc = player.GetComponent<PlayerController>();

        pc.BuffSpeed = speed;
        pc.buffType = PlayerController.BuffType.Speed;
        pc.buffTime = timeLimit;
        base.OnPickUp(player);
    }
    
    
}
