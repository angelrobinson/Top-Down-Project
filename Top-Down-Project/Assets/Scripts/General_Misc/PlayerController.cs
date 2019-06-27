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

    ///// <summary>
    ///// Used when IK is set to be used with animations
    ///// </summary>
    //protected virtual void OnAnimatorIK()
    //{
    //    //if there is no weapon equipped return
    //    if (!controlled.currentWeapon)
    //        return;

    //    //make temp weapon so that I don't have to do getcomponent each time
    //    Weapon current = controlled.currentWeapon.GetComponent<ProjectileWeapon>();

    //    //if there is a transform assigned to the IK for right hand do the following
    //    if (current.RightHandIK)
    //    {
    //        controlled.CharAnimator.SetIKPosition(AvatarIKGoal.RightHand, current.RightHandIK.position);
    //        controlled.CharAnimator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1f);
    //        controlled.CharAnimator.SetIKRotation(AvatarIKGoal.RightHand, current.RightHandIK.rotation);
    //        controlled.CharAnimator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1f);

    //        //if there is a transform assigned to the IK for Hint. . . This is for the right elbow
    //        if (current.RightElbowHint)
    //        {
    //            controlled.CharAnimator.SetIKHintPosition(AvatarIKHint.RightElbow, current.RightElbowHint.position);
    //            controlled.CharAnimator.SetIKHintPositionWeight(AvatarIKHint.RightElbow, 1f);
    //        }



    //    }
    //    else
    //    {
    //        controlled.CharAnimator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0f);
    //        controlled.CharAnimator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0f);

    //        //if there is a transform assigned to the IK for Hint. . . This is for the right elbow
    //        if (current.RightElbowHint)
    //        {
    //            controlled.CharAnimator.SetIKHintPositionWeight(AvatarIKHint.RightElbow, 0f);
    //        }
    //    }

    //    //if there is a transform assigned to the IK for left hand do the following
    //    if (current.LeftHandIK)
    //    {
    //        controlled.CharAnimator.SetIKPosition(AvatarIKGoal.LeftHand, current.LeftHandIK.position);
    //        controlled.CharAnimator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1f);
    //        controlled.CharAnimator.SetIKRotation(AvatarIKGoal.LeftHand, current.LeftHandIK.rotation);
    //        controlled.CharAnimator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1f);

    //        //if there is a transform assigned to the IK for Hint. . . This is for the left elbow
    //        if (current.LeftElbowHint)
    //        {
    //            controlled.CharAnimator.SetIKHintPosition(AvatarIKHint.LeftElbow, current.LeftElbowHint.position);
    //            controlled.CharAnimator.SetIKHintPositionWeight(AvatarIKHint.LeftElbow, 1f);
    //        }
    //    }
    //    else
    //    {
    //        controlled.CharAnimator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0f);
    //        controlled.CharAnimator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0f);

    //        //if there is a transform assigned to the IK for Hint. . . This is for the left elbow
    //        if (current.LeftElbowHint)
    //        {
    //            controlled.CharAnimator.SetIKHintPositionWeight(AvatarIKHint.LeftElbow, 0f);
    //        }
    //    }
    //}
}
