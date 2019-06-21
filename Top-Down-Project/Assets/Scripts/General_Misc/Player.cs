using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ObjectHealth))]
public class Player : Character
{
    [Header("Movement")]
    [SerializeField] float _speed;

    [Header("Stats")]
    [SerializeField] float _stamina;

    [Header("Equipment")]
    [SerializeField] ProjectileWeapon[] guns;
    [SerializeField] MeleeWeapon[] melee;

    //[Header("Equipment")]
    //public Weapon currentWeapon;
    //public Weapon noWeapon;
    //public ProjectileWeapon shotgun;
    //public ProjectileWeapon oneHanded;
    //public ProjectileWeapon rifle;

    #region Properties
    public float Stamina   
    {
        get { return _stamina; }
        set
        {
            //can not be below 0
            if (value < 0)
            {
                _stamina = 0;
            }

            _stamina = value;
        }
    }
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

    public ObjectHealth Health { get; set; }
    #endregion

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

        Health = GetComponent<ObjectHealth>();
        
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
