using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [Header("Stats")]
    [SerializeField, Range(10.0f,100.0f)] private int Health;

    // Start is called before the first frame update
    void Start()
    {       

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
    /// Allows to set specific target object or Vector direction to have your game object turn to look at it
    /// </summary>
    /// <param name="target">Where you do you want the gameobject to face</param>
    public override void TurnPlayer(Vector3 target)
    {
        transform.LookAt(target);
    }

    /// <summary>
    /// Allows to make your character jump either in place or while moving
    /// </summary>
    public override void Jump()
    {
        if (anim.GetBool("IsMoving") == true)
        {
            anim.SetTrigger("RunJump");
        }
        else
        {
            base.Jump();
        }

    }
}
