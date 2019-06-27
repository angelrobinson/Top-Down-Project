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
    //bool dead;

    //[Header("Equipment")]
    //[SerializeField] ProjectileWeapon[] guns;
    //[SerializeField] MeleeWeapon[] melee;
    //public Weapon currentWeapon;

    //[Header("Equipment")]
    //public GameObject currentWeapon;
    //public GameObject weaponHolder;
    ProjectileWeapon gun;

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

    //public ObjectHealth MyHealth { get; set; }
    #endregion

    // Start is called before the first frame update
    void Start()
    {       

        ////if animator is not set in inspector try and get the animator on the object this script is attached to
        //if (anim == null)
        //{
        //    anim = GetComponent<Animator>();

        //    //if there is no animator on the same object look for it on the Parent of the object
        //    if (anim == null)
        //    {
        //        anim = GetComponentInParent<Animator>();
        //    }
        //}

        //MyHealth = GetComponent<ObjectHealth>();
        
    }

    private void Update()
    {
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

    ///// <summary>
    ///// Used to Equip a weapon.
    ///// </summary>
    ///// <param name="weapon">Gameobject of weapon that is to be equipped</param>
    //public void EquipWeapon(GameObject weapon)
    //{
    //    if (currentWeapon)
    //    {
    //        GameObject temp = currentWeapon;
    //        Destroy(temp.gameObject);

    //    }

    //    currentWeapon = Instantiate(weapon);
    //    currentWeapon.transform.SetParent(weaponHolder.transform);
    //    currentWeapon.transform.localPosition = weapon.transform.position;
    //    currentWeapon.transform.localRotation = weapon.transform.localRotation;
    //    currentWeapon.gameObject.layer = gameObject.layer;

    //    anim.SetInteger("WeaponState", (int)weapon.GetComponent<Weapon>().weaponState);
    //}

    /// <summary>
    /// This method checks to see if the Player health gets to zero or below and if the player is already dead.
    /// If not already dead, it will trigger the death animation and mark the player as dead
    /// </summary>
    public override void Die()
    {        
        if (MyHealth.Health <= 0 && dead == false)
        {
            base.Die();
            dead = true;
        }
        
    }
    #endregion
}
