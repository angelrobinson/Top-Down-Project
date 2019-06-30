using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    Transform trans;
    public Transform lookAtTarget;
    public Vector3 offset;
    

    // Start is called before the first frame update
    void Start()
    {
        trans = GetComponent<Transform>();

        //if no offset is input into the inspector, it will default to starting position Vector of the object that it is attached to
        if (offset == Vector3.zero)
        {
            offset = trans.position;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (lookAtTarget)
        {
            //move the camera to the offset position related to the target and look at the target
            trans.position = lookAtTarget.position + offset;
            trans.LookAt(lookAtTarget);
        }
        

    }
}
