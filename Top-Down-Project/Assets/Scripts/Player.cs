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
        anim.SetBool("IsMoving", true);
    }

    public void TurnPlayer(Vector3 target)
    {
        trans.LookAt(target);
    }

    public void Jump()
    {
        if (anim.GetBool("IsMoving") == true)
        {
            anim.SetTrigger("RunJump");
        }
        else
        {
            anim.SetTrigger("Jump");
        }

    }
}
