using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// we want to keep our designers from creating objects that are just Pickups, 
/// since the Pickup script doesn’t actually do anything when picked up; we only want the derived versions to be used.
/// We can restrict the use of the base class by changing the Pickup to an abstract class. 
/// This tells the compiler “this class can only be used if something derives from it”.
/// </summary>
public abstract class ObjController : MonoBehaviour
{
    protected Player controlled;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
