using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum BuffType { None, Speed } //idea:  add MaxHealth buff

    [Header("Player/Controlled Settings")]
    Player controlled;
    [SerializeField] float walkSpeed;
    [SerializeField] float runSpeed;
    
    [Header("Buff settings")]
    public BuffType buffType = BuffType.None;
    public float buffTime;
    float buffSpeed;

    public float BuffSpeed { get { return buffSpeed; } set { buffSpeed = value; } }

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

    private void Update()
    {
        //check on Player's health and trigger die animation when health gets to zero
        if (controlled.MyHealth.Health <= 0)
        {
            controlled.Die();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (controlled.MyHealth.Health > 0)
        {
            FindMouseLocation();
        }
        
        
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

        
        //If there is a buff, update the player object as needed for the buff
        switch (buffType)
        {
            case BuffType.None:
                //use run speed/animation if  the shift key is pressed while moving, otherwise use the walking speed
                if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                {
                    controlled.Speed = runSpeed;
                }
                else
                {
                    controlled.Speed = walkSpeed;
                }
                break;
            case BuffType.Speed:
                if (buffTime >= 0)
                {
                    buffTime -= Time.deltaTime;

                    //use run speed/animation if  the shift key is pressed while moving, otherwise use the walking speed
                    if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                    {
                        controlled.Speed = runSpeed + BuffSpeed;
                    }
                    else
                    {
                        controlled.Speed = walkSpeed + BuffSpeed;
                    }
                }
                else
                {
                    buffType = BuffType.None;
                }
                break;
        }


        controlled.MovePlayer(contMovement, controlled.Speed);

        //jump when you press the key set in the Input settings...default is space
        if (Input.GetButtonDown("Jump"))
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
