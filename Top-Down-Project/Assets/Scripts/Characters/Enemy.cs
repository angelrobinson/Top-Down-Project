using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(LootTable))]
public class Enemy : Character
{
    [Header("Navigation")]
    [SerializeField] Transform target;
    NavMeshAgent agent;    
    Vector3 desiredVel;

    //Transform player;

    [Header("Loot Settings")]
    [SerializeField] LootTable loot;
    [SerializeField, Range(0, 1)]
    float chanceToDrop;
    

    new protected void Awake()
    {
        base.Awake();
        agent = GetComponent<NavMeshAgent>();
        //player = GameObject.FindGameObjectWithTag("Player").transform;
        
        loot = GetComponent<LootTable>();

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

    public override void Update()
    {
        base.Update();

        if (GameManager.Player)
        {
            target = GameManager.Player.transform;
        }
        
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
                if (Vector3.Distance(transform.position, GameManager.Player.transform.position) <= gun.MaxDistance)
                {
                    //check the angle between the Enemy’s forward and the Player.
                    //If the angle is below the equipped weapon’s attackAngle we have the Enemy pull the trigger.
                    float checkAngle = Vector3.Angle(transform.forward, GameManager.Player.transform.position);
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

        //if (MyHealth.Health == 0)
        //{
        //    Die();
        //}
        if (MyHealth.Health <= 0 && dead == false)
        {
            Die();
            DropItem();
        }

    }

    #region Helper Methods

    /// <summary>
    /// make sure that the Navemesh agent uses the velocity of the animator
    /// </summary>
    private void OnAnimatorMove()
    {
        agent.velocity = CharAnimator.velocity;
    }


    /// <summary>
    /// Stop the navMesh agent and turn on Ragdoll mechanics
    /// </summary>
    protected override void RagdollOn()
    {
        agent.isStopped = true;
        base.RagdollOn();
    }

    /// <summary>
    /// Turns off ragdoll mechanics and makes sure the navMesh agent is running
    /// </summary>
    protected override void RagdollOff()
    {
        agent.isStopped = false;
        base.RagdollOff();
    }

    /// <summary>
    /// Drop loot item and then proceed with base method
    /// </summary>
    //public override void Die()
    //{
    //    DropItem();
    //    base.Die();
    //}

    //drop and item in the loot table
    void DropItem()
    {
        if (Random.Range(0f,1.0f) < chanceToDrop)
        {
            GameObject go = loot.Select(loot.table);
            //GameObject go = loot.Select();
            Instantiate(go, transform.position, go.transform.rotation);
            //Instantiate(loot.Select(), transform.position, Quaternion.identity);
        }
    }

    //required method
    public override void TurnPlayer(Vector3 target)
    {
        throw new System.NotImplementedException();
    }
    #endregion  
}
