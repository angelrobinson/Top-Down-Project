using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHealth : MonoBehaviour
{
    //variables
    [SerializeField] int _maxHealth;
    [SerializeField] int _currentHealth;



    #region Properties

    /// <summary>
    /// gets percentage of current health compared to max health
    /// </summary>
    //public float HealthPercent { get { return Health / MaxHealth; } }
    
    /// <summary>
    /// can't be zero as that would mean the object is always dead
    /// normally this would be set on game start (i.e. in inspector),
    /// but if we decide to include character leveling and want to increase max health pool, 
    /// this is where it would be
    /// </summary>
    public int MaxHealth
    {
        get { return _maxHealth; }
        private set
        {            
            if (value <= 0)
            {
                _maxHealth = 100;
            }
            else
            {
                MaxHealth = value;
            }
        }
    }

    /// <summary>
    /// Current health of an object.
    /// When it hits zero, it means the object is dead
    /// If a value lower than zero is passed, the current health will default to zero
    /// if a value higher than the maxhealth pool, the current health will default to the max health
    /// If wanting to adjust the current health based off of  damage or healing, you need to do this from a helper method
    /// </summary>
    public int Health
    {
        get { return _currentHealth; }
        private set
        {
            //Health of an object can't be zero or it would be dead
            if (value < 0)
            {
                _currentHealth = 0;
            }
            else if (value > MaxHealth)
            {
                _currentHealth = MaxHealth;
            }
            else
            {
                _currentHealth = value;
            }
        }
    }
    #endregion

    private void Awake()
    {
        //set initial amount of health
        Health = MaxHealth;
    }

    private void OnEnable()
    {
        //set initial amount of health
        Health = MaxHealth;
    }
    #region Helper Methods
    /// <summary>
    /// Use this to reduce the current health of the object.
    /// </summary>
    /// <param name="dmgAmt">Amount to reduce the current health by</param>
    public void TakeDamage(int dmgAmt)
    {
        //make sure that the dmg amount is a possitive number
        dmgAmt = Mathf.Abs(dmgAmt);

        //update the current health by subtracting dmgAmt
        Health -= dmgAmt;
    }

    /// <summary>
    /// Use this to increase the current health of the object
    /// </summary>
    /// <param name="healAmt">Amount to increase the current health by</param>
    public void Heal(int healAmt)
    {
        //make sure that the heal amount is a possitive number
        healAmt = Mathf.Abs(healAmt);

        //update the current health by adding healAmt
        Health += healAmt;
    }


    /// <summary>
    /// Use this to assign the MaxHealth at start of game
    /// or, if you have a leveling system, when your character levels.
    /// </summary>
    /// <param name="newAmt">Full amount you want the max health to be</param>
    public void NewMaxHealth(int newAmt)
    {
        //save the current maxHealth to a temp variable to do any calculations if needed
        int currentMax = MaxHealth;


        //if the newAmount is zero or is the same amount as the currentMax then we don't need to change the MaxHealth
        if (newAmt <= 0 || newAmt == currentMax)            
        {
            return;
        }

        //assign full new amount to MaxHealth
        MaxHealth = newAmt;
    }

    /// <summary>
    /// Use this to adjust the MaxHealth based on a percentage amount of the base MaxHealth to Increase
    /// </summary>
    /// <param name="percentAmt">percent amount in decimal format to increase the MaxHealth by</param>
    public void NewMaxHealth(float percentAmt)
    {
        //insure that the percent amt is a positive number
        percentAmt = Mathf.Abs(percentAmt);

        //save the current maxHealth to a temp variable to do any calculations if needed
        int currentMax = MaxHealth;

        
        //if the percent amount is zero then we don't need to adjust the MaxHealth
        if (percentAmt == 0.0f)
        {
            return;
        }

        //calculate how much to add to the MaxHealth
        float changeAmt = MaxHealth * percentAmt;

        //add the new amount to the MaxHealth
        MaxHealth += (int) changeAmt;
    }

    public float HealthPercent() {  return (float)Health / MaxHealth * 100; } 
    #endregion
}
