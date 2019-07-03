using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ObjectHealth))]
public class Player : Character
{
    [Header("Movement")]
    [SerializeField] float _speed;

    ProjectileWeapon gun;

    #region Properties
    
    /// <summary>
    /// Use this to get and set the speed of the character
    /// when at Zero, the character can not move
    /// </summary>
    public float Speed
    {
        get { return _speed; }
        set
        {
            //can not be below 0
            if (value < 0)
            {
                _speed = 0;
            }

            _speed = value;
        }
    }
    #endregion


    new private void Start()
    {
        UIController.Instance.PlayerHealthBar(this);
    }



    public override void Update()
    {
        base.Update();

        //UIController.Instance.PlayerHealthBar(this);

        if (currentWeapon)
        {
            gun = currentWeapon.GetComponent<ProjectileWeapon>();
            //if can shoot and if the button set up in the Input settings for Fire1 is pressed, instantiate the bullet
            if (gun.canShoot)
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    gun.PullTrigger();
                }
            }
        }
    }


    #region helper methods
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

    /// <summary>
    /// This method checks to see if the Player health gets to zero or below and if the player is already dead.
    /// If not already dead, it will trigger the death animation and mark the player as dead
    /// </summary>
    public override void Die()
    {        
        if (dead == false)
        {
            base.Die();
        }
        
    }
    #endregion
}
