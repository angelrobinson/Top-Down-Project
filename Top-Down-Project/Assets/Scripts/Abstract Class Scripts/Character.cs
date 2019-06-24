using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// we want to keep our designers from creating objects that are just Pickups, 
/// since the Pickup script doesn’t actually do anything when picked up; we only want the derived versions to be used.
/// We can restrict the use of the base class by changing the Pickup to an abstract class. 
/// This tells the compiler “this class can only be used if something derives from it”.
/// </summary>
public abstract class Character : MonoBehaviour
{
    //variables
    [Tooltip("Reference to the Animator component that will be controlling animations for this game object." +
        "If the animator is on the same object or it's direct parent object, you can leave this blank")]
    [SerializeField]protected Animator anim;

    //properties
    public Animator CharAnimator { get {return anim; } }

    #region Helper Methods
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
    /// Derived class will control this
    /// Allows to set specific target object or Vector direction to have your game object turn to look at it
    /// </summary>
    /// <param name="target">Where do you want the gameobject to face?</param>
    public abstract void TurnPlayer(Vector3 target);

    /// <summary>
    /// Allows to make your character jump by triggering Jump animation
    /// </summary>
    public virtual void Jump()
    {
        anim.SetTrigger("Jump");
    }

    /// <summary>
    /// Triggers death animations
    /// </summary>
    public virtual void Die()
    {
        anim.SetTrigger("Death");
    }
    #endregion
}
