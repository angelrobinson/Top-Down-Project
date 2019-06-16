using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator anim;
    [HideInInspector] public Transform trans;
    

    // Start is called before the first frame update
    void Start()
    {
        trans = GetComponent<Transform>();

        if (anim == null)
        {
            anim = GetComponent<Animator>();
        }
        
    }


    public void MovePlayer(Vector3 direction, float speed)
    {
        anim.SetFloat("Vertical", direction.z * speed);
        anim.SetFloat("Horizontal", direction.x * speed);
    }

    public void TurnPlayer(Vector3 target)
    {
        trans.LookAt(target);
    }
}
