using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : Character
{
    //Animator ai;
    NavMeshAgent agent;
    [SerializeField] Transform target;
    Vector3 desiredVel;
    //ObjectHealth myHealth;
    //bool dead;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        CharAnimator = GetComponent<Animator>();
        //myHealth = GetComponent<ObjectHealth>();
    }

    private void Update()
    {
        //tell agent to go to target. 
        //if target is moving, this means the agent "pursues" the target
        agent.SetDestination(target.position);

        //add animation to the nav mesh agent
        desiredVel = Vector3.MoveTowards(desiredVel, agent.desiredVelocity, agent.acceleration * Time.deltaTime);
        Vector3 input = transform.InverseTransformDirection(desiredVel);
        //Vector3 input = agent.desiredVelocity;
        //input = transform.InverseTransformDirection(input);
        //CharAnimator.SetFloat("Horizontal", input.x);
        //CharAnimator.SetFloat("Vertical", input.z);
        MovePlayer(input);

        if (MyHealth.Health == 0)
        {
            Die();
        }
    }

    /// <summary>
    /// make sure that the Navemesh agent uses the velocity of the animator
    /// </summary>
    private void OnAnimatorMove()
    {
        agent.velocity = CharAnimator.velocity;
    }

    public override void TurnPlayer(Vector3 target)
    {
        transform.LookAt(target);
    }

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
}
