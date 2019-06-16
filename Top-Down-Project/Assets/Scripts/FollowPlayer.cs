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
        if (offset == Vector3.zero)
        {
            offset = trans.position;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
        //cameraTargetPosition = Vector3.ClampMagnitude(cameraTargetPosition, 1.0f);
        trans.position = lookAtTarget.position + offset;
        trans.LookAt(lookAtTarget);

    }
}
