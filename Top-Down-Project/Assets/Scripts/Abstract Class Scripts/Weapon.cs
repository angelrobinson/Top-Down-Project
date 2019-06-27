using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    /// <summary>
    /// Enum created to use with Animation and IK
    /// </summary>
    public enum WeaponState
    {
        None = 0,
        Rifle = 1,
        Pistol = 2
    }

    [Header("Weapon Settings")]
    public WeaponState weaponState = WeaponState.None;

    [Header("AI Settings"), Range(-90,90)]
    public float aimingAngleDegree;

    [Header("Inverse Kenetics (IK) Settings")]
    public Transform LeftHandIK;
    public Transform RightHandIK;
    public Transform RightElbowHint;
    public Transform LeftElbowHint;

    /// <summary>
    /// derived class will determine what is done in this method
    /// </summary>
    public abstract void PullTrigger();
 
}





