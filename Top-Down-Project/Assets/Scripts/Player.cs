using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator anim;
    [HideInInspector] public Transform trans;
    

    // Start is called before the first frame update
    void Start()
    {
        trans = GetComponent<Transform>();

        //if animator is not set in inspector try and get the animator on the object this script is attached to
        if (anim == null)
        {
            anim = GetComponent<Animator>();

            //if there is no animator on the same object look for it on the Parent of the object
            if (anim == null)
            {
                anim = GetComponentInParent<Animator>();
            }
        }
        
    }


    /// <summary>
    /// Allows for movement of an object in a specific direction and speed.
    /// This method was constructed to allow dynamic input, where you can get the directions as Input.GetAxis or any vector direction
    /// and speed can be slower for walking and faster for running. 
    /// </summary>
    /// <param name="direction">Input Direction or Axis</param>
    /// <param name="speed">speed of movement</param>
    public void MovePlayer(Vector3 direction, float speed)
    {
        anim.SetFloat("Vertical", direction.z * speed);
        anim.SetFloat("Horizontal", direction.x * speed);
    }

    /// <summary>
    /// Allows to set specific target object or Vector direction to have your game object turn to look at it
    /// </summary>
    /// <param name="target">Where you do you want the gameobject to face</param>
    public void TurnPlayer(Vector3 target)
    {
        trans.LookAt(target);
    }

    /// <summary>
    /// Allows to make your character jump either in place or while moving
    /// </summary>
    public void Jump()
    {
        if (anim.GetBool("IsMoving") == true)
        {
            anim.SetTrigger("RunJump");
        }
        else
        {
            anim.SetTrigger("Jump");
        }

    }
}
