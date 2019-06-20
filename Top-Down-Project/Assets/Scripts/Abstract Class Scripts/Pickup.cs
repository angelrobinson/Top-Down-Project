using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// we want to keep our designers from creating objects that are just Pickups, 
/// since the Pickup script doesn’t actually do anything when picked up; we only want the derived versions to be used.
/// We can restrict the use of the base class by changing the Pickup to an abstract class. 
/// This tells the compiler “this class can only be used if something derives from it”.
/// </summary>
public abstract class Pickup : MonoBehaviour
{
    [SerializeField] protected float lifespan = 15.0f;
    [SerializeField] protected Vector3 rotation;
    [SerializeField] protected Space relativeRotation;

    private void Awake()
    {
        //destroy the object after set amount of seconds defined in lifespan variable
        Destroy(gameObject, lifespan);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {        
        //rotate the object on the Y axis
        //transform.Rotate(Vector3.up, 0.25f, Space.Self);

        //rotate the object based off of a Vector3 eulers
        
        transform.Rotate(rotation, Space.World);
        //transform.Rotate(0.12f, 0.25f, 0.35f, Space.Self);
        
    }

    /// <summary>
    /// collider on object needs to be set to trigger
    /// </summary>
    /// <param name="collider">the collider that enters this objects trigger collider</param>
    protected void OnTriggerEnter(Collider collider)
    {
        Debug.Log("Collided with: " + collider.name);
        //check to see if other collider has the Player script on it as a component
        Player player = collider.GetComponent<Player>();
        if (player)
        {
            //if the player script was found, run the pick up method
            OnPickUp(player);
        }
    }

    /// <summary>
    /// Destory object if the Player passes through the trigger collider
    /// This method is set to protected virtual so that derived classes can access and override this method
    /// </summary>
    /// <param name="player"></param>
    protected virtual void OnPickUp(Player player)
    {
        Destroy(gameObject);
    }
}
