using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Player controlled;
    public float walkSpeed;
    public float runSpeed;
        

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
    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        //input movement when arow keys or WSAD is pressed the character will move on the x and z axis'
        Vector3 contMovement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        //clamp the base movment to be between 0.0 - 0.1
        contMovement = Vector3.ClampMagnitude(contMovement, 1);

        //ensure that the player only moves on x and z axis
        contMovement = controlled.trans.InverseTransformDirection(contMovement);

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            controlled.MovePlayer(contMovement, runSpeed);
        }
        else
        {
            controlled.MovePlayer(contMovement, walkSpeed);
        }
    }
}
