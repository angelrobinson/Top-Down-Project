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

    [Header("Loot Settings")]
    [SerializeField] LootTable loot;
    [SerializeField, Range(0, 1)]
    float chanceToDrop;
    bool lootDropped;
    

    new protected void Awake()
    {
        base.Awake();
        agent = GetComponent<NavMeshAgent>();
        
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

    new private void Start()
    {
        base.Start();
        UIController.Instance.EnemyHealthBar(this);
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

        
        if (MyHealth.Health <= 0 && dead == false)
        {
            Die();
            DropItem();
        }

    }

    #region Helper Methods

    public override void Die()
    {
        GameManager.Instance.UpdateScore();
        base.Die();
    }
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

    //drop and item in the loot table
    void DropItem()
    {
        float drop = Random.Range(0f, 1.0f);
        if (drop < chanceToDrop)
        {
            GameObject go = loot.Select(loot.table);
            //GameObject go = loot.Select();
            Instantiate(go, transform.position, go.transform.rotation);
        }
    }

    //required method
    public override void TurnPlayer(Vector3 target)
    {
        throw new System.NotImplementedException();
    }
    #endregion  
}
