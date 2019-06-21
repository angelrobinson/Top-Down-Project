﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum BuffType { None, Speed } //idea:  add MaxHealth buff

    Player controlled;
    [SerializeField] float walkSpeed;
    [SerializeField] float runSpeed;

    public bool isBuffed = false;
    public BuffType buffType = BuffType.None;
    public float buffTime;
        

    // Start is called before the first frame update
    void Start()
    {
        //set defaults if the inspector info is not put in
        if (controlled == null)
        {
            controlled = GetComponentInChildren<Player>();
        }

        if (walkSpeed == 0)
        {
            walkSpeed = 4;
        }

        if (runSpeed == 0)
        {
            runSpeed = 8;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        FindMouseLocation();
        
        Movement();
    }

    private void Movement()
    {
        //input movement when arow keys or WSAD is pressed the character will move on the x and z axis'
        Vector3 contMovement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        //clamp the base movment to be between 0.0 - 0.1
        contMovement = Vector3.ClampMagnitude(contMovement, 1);

        //ensure that the player only moves on x and z axis
        contMovement = controlled.transform.InverseTransformDirection(contMovement);

        //check to see if player is moving and if so set animation bool to true as to call the correct jum animation when triggered
        if (contMovement.x != 0 || contMovement.z != 0)
        {
            controlled.CharAnimator.SetBool("IsMoving", true);
        }

        //if (isBuffed)
        //{
        //    switch (buffType)
        //    {
        //        case BuffType.None:
        //            break;
        //        case BuffType.Speed:
        //            if (buffTime <= 0)
        //            {
        //                walkSpeed += controlled.Speed - walkSpeed;
        //                runSpeed += controlled.Speed - runSpeed;
        //            }
        //            else
        //            {
        //                float previousWalk = walkSpeed;
        //                float previousRun = runSpeed;
        //                while (buffTime > 0)
        //                {
        //                    walkSpeed += controlled.Speed - walkSpeed;
        //                    runSpeed += controlled.Speed - runSpeed;
        //                }

        //                walkSpeed = previousWalk;
        //                runSpeed = previousRun;
        //            }


        //            break;
        //        default:
        //            break;
        //    }
        //}

        //use run speed/animation if  the shift key is pressed while moving, otherwise use the walking speed
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            controlled.Speed = runSpeed;
            controlled.MovePlayer(contMovement, runSpeed);
        }        
        else
        {
            controlled.Speed = walkSpeed;
            controlled.MovePlayer(contMovement, walkSpeed);
        }

        

        //jump when you press space key
        if (Input.GetKeyDown(KeyCode.Space))
        {
            controlled.Jump();
        }
        
    }

    private void FindMouseLocation()
    {
        //create invisible plane to attach to the mouse position
        Plane plane = new Plane(Vector3.up, transform.position);

        //create a ray from camera to mouse position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //calculate distance from camera to the plane that the ray hit
        float distance;
        if (plane.Raycast(ray, out distance))
        {            
            controlled.TurnPlayer(ray.GetPoint(distance));
        }

        
    }
}
