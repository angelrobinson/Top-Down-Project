using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// we want to keep our designers from creating objects that are just Pickups, 
/// since the Pickup script doesn’t actually do anything when picked up; we only want the derived versions to be used.
/// We can restrict the use of the base class by changing the Pickup to an abstract class. 
/// This tells the compiler “this class can only be used if something derives from it”.
/// </summary>

 [RequireComponent(typeof(Animator))]
public abstract class Character : MonoBehaviour
{
    //variables
    [Tooltip("Reference to the Animator component that will be controlling animations for this game object." +
        "If the animator is on the same object or it's direct parent object, you can leave this blank")]
    [SerializeField]protected Animator anim;

    protected bool dead;

    //TODO: possible uses of multiple guns in inventory?
    //[Header("Equipment")]
    //[SerializeField] ProjectileWeapon[] guns;
    //[SerializeField] MeleeWeapon[] melee;
    //public Weapon currentWeapon;

    [Header("Equipment")]
    [SerializeField]protected ProjectileWeapon[] guns; //gun inventory
    [SerializeField] protected GameObject currentWeapon;
    [SerializeField] protected GameObject weaponHolder;

    //properties
    public Animator CharAnimator { get { return anim; } protected set { anim = value; } }
    public ObjectHealth MyHealth { get; set; }

    protected void Awake()
    {
        //if animator is not set in inspector try and get the animator on the object this script is attached to
        if (CharAnimator == null)
        {
            CharAnimator = GetComponent<Animator>();

            //if there is no animator on the same object look for it on the Parent of the object
            if (CharAnimator == null)
            {
                CharAnimator = GetComponentInParent<Animator>();
            }
        }

        MyHealth = GetComponent<ObjectHealth>();
    }

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
    /// Allows for movement of an object in a specific direction
    /// This method was constructed to work with NavMesh AI where you put the speed/velocity into the direction Vector3
    /// </summary>
    /// <param name="direction">Input Direction</param>
    public void MovePlayer(Vector3 direction)
    {
        anim.SetFloat("Vertical", direction.z);
        anim.SetFloat("Horizontal", direction.x);
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

    /// <summary>
    /// Used to Equip a weapon.
    /// </summary>
    /// <param name="weapon">Gameobject of weapon that is to be equipped</param>
    public void EquipWeapon(GameObject weapon)
    {
        //if there is already a weapon equipped, swap them out (Or, have an inventory and a swap button)
        if (currentWeapon)
        {
            GameObject temp = currentWeapon;
            Destroy(temp.gameObject);

        }

        //instantiate the weapon, set parent and layer
        currentWeapon = Instantiate(weapon);
        currentWeapon.transform.SetParent(weaponHolder.transform);
        currentWeapon.transform.localPosition = weapon.transform.position;
        currentWeapon.transform.localRotation = weapon.transform.localRotation;
        currentWeapon.gameObject.layer = gameObject.layer;

        anim.SetInteger("WeaponState", (int)weapon.GetComponent<Weapon>().weaponState);
    }
    #endregion

    /// <summary>
    /// Used when IK is set to be used with animations
    /// </summary>
    protected virtual void OnAnimatorIK()
    {
        //if there is no weapon equipped return
        if (!currentWeapon)
            return;

        //make temp weapon so that I don't have to do getcomponent each time
        Weapon current = currentWeapon.GetComponent<ProjectileWeapon>();

        //if there is a transform assigned to the IK for right hand do the following
        if (current.RightHandIK)
        {
            CharAnimator.SetIKPosition(AvatarIKGoal.RightHand, current.RightHandIK.position);
            CharAnimator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1f);
            CharAnimator.SetIKRotation(AvatarIKGoal.RightHand, current.RightHandIK.rotation);
            CharAnimator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1f);

            //if there is a transform assigned to the IK for Hint. . . This is for the right elbow
            if (current.RightElbowHint)
            {
                CharAnimator.SetIKHintPosition(AvatarIKHint.RightElbow, current.RightElbowHint.position);
                CharAnimator.SetIKHintPositionWeight(AvatarIKHint.RightElbow, 1f);
            }



        }
        else
        {
            CharAnimator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0f);
            CharAnimator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0f);

            //if there is a transform assigned to the IK for Hint. . . This is for the right elbow
            if (current.RightElbowHint)
            {
                CharAnimator.SetIKHintPositionWeight(AvatarIKHint.RightElbow, 0f);
            }
        }

        //if there is a transform assigned to the IK for left hand do the following
        if (current.LeftHandIK)
        {
            CharAnimator.SetIKPosition(AvatarIKGoal.LeftHand, current.LeftHandIK.position);
            CharAnimator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1f);
            CharAnimator.SetIKRotation(AvatarIKGoal.LeftHand, current.LeftHandIK.rotation);
            CharAnimator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1f);

            //if there is a transform assigned to the IK for Hint. . . This is for the left elbow
            if (current.LeftElbowHint)
            {
                CharAnimator.SetIKHintPosition(AvatarIKHint.LeftElbow, current.LeftElbowHint.position);
                CharAnimator.SetIKHintPositionWeight(AvatarIKHint.LeftElbow, 1f);
            }
        }
        else
        {
            CharAnimator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0f);
            CharAnimator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0f);

            //if there is a transform assigned to the IK for Hint. . . This is for the left elbow
            if (current.LeftElbowHint)
            {
                CharAnimator.SetIKHintPositionWeight(AvatarIKHint.LeftElbow, 0f);
            }
        }
    }
}
