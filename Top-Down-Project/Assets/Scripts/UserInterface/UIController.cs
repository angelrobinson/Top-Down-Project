using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIController : MonoBehaviour
{
    //Canvas items
    GameObject panel;
    [SerializeField] Text healthPercent;

    //Variables-Properties
    Player playerHealth;

    private void Awake()
    {
        //find the game object with the tag "Player" and get the health coponent
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        //press escape to bring up popup panel
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            panel.SetActive(true);
        }

        Debug.Log("health: " + playerHealth.Health.Health);
        Debug.Log("Max: " + playerHealth.Health.MaxHealth);
        Debug.Log("Health %: " + playerHealth.Health.HealthPercent);
        //healthPercent.text = string.Format("Health: {0}%", Mathf.RoundToInt(playerHealth.HealthPercent * 100f));
        healthPercent.text = string.Format("Health: {0}%", playerHealth.Health.HealthPercent * 100f);
    }

    
    /// <summary>
    /// To be called when clicking the Yes button in the panel that is shown when pressing esc key 
    /// </summary>
    public void ExitApplication()
    {
        Application.Quit();
    }
}
/*
 * public CharacterHealth health;
    private Text text;

    private void Awake()
    {
        text = GetComponent<Text>();
    }

    private void Update()
    {
        text.text = string.Format("Health: {0}%", Mathf.RoundToInt(health.HealthPercent * 100f));
    }
*/