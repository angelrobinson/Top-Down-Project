using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : Character
{
    [Header("Navigation")]
    [SerializeField] Transform target;
    NavMeshAgent agent;    
    Vector3 desiredVel;

    Transform player;
    

    new private void Awake()
    {
        base.Awake();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = GameObject.FindGameObjectWithTag("Player").transform;

        int index = 0;
        //try and equip a random weapon from the guns array
        try
        {
            index = Random.Range(0, guns.Length);
            EquipWeapon(guns[index].gameObject);
        }
        catch (System.NullReferenceException ex)
        {
            Debug.LogError("NO WEAPON WAS FOUND IN INDEX " + index + "! " + ex.Message);
            
        }
        catch (System.IndexOutOfRangeException iex)
        {
            Debug.LogError("NO WEAPONS ARE IN INVENTORY! " + iex.Message);
        }
        
    }

    private void Update()
    {
        //if there is not a target to move to, stop the animator and end the update method early
        //so we don't get null reference errors
        if (!target)
        {
            agent.isStopped = true;
            return;
        }

        if (currentWeapon)
        {
            //check to see if the current weapon is a projectile weapon
            ProjectileWeapon gun = currentWeapon.GetComponent<ProjectileWeapon>();
            if (gun)
            {
                //check to see if the player is within max distance of the gun range
                if (Vector3.Distance(transform.position, player.position) <= gun.MaxDistance)
                {
                    //check the angle between the Enemy’s forward and the Player.
                    //If the angle is below the equipped weapon’s attackAngle we have the Enemy pull the trigger.
                    float checkAngle = Vector3.Angle(transform.forward, player.position);
                    //Debug.Log("Aim Angle: " + checkAngle);
                    if (checkAngle <= gun.aimingAngleDegree)
                    {
                        if (gun.canShoot)
                        {
                            gun.PullTrigger();
                            
                        }
                        
                    }
                }
                
            }
        }
        

        //tell agent to go to target. 
        //if target is moving, this means the agent "pursues" the target
        agent.SetDestination(target.position);

        //add animation to the nav mesh agent
        desiredVel = Vector3.MoveTowards(desiredVel, agent.desiredVelocity, agent.acceleration * Time.deltaTime);
        Vector3 input = transform.InverseTransformDirection(desiredVel);        
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
            Destroy(gameObject);
            dead = true;
        }

    }

    protected override void RagdollOn()
    {
        CharAnimator.enabled = false;
        base.RagdollOn();
    }

    protected override void RagdollOff()
    {
        base.RagdollOff();
        CharAnimator.enabled = true;
    }
}
