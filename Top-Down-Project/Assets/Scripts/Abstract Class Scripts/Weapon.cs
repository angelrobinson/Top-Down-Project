using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public enum WeaponState
    {
        None = 0,
        Rifle = 1,
        Pistol = 2
    }

    [Header("Weapon Settings")]
    public WeaponState weaponState = WeaponState.None;

    [Header("Inverse Kenetics (IK) Settings")]
    public Transform LeftHandIK;
    public Transform RightHandIK;

    public abstract void PullTrigger();
    public abstract void ReleaseTrigger();
 
}





