using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIController : MonoBehaviour
{
    //Canvas items
    [SerializeField] GameObject panel;
    [SerializeField] Text healthPercent;

    //Variables-Properties
    public ObjectHealth playerHealth;

    private void Awake()
    {
        //find the Player and get it's health component
        playerHealth = GameManager.Player.GetComponent<ObjectHealth>();
    }
    // Update is called once per frame
    void Update()
    {
        if (GameManager.Player && !playerHealth)
        {
            playerHealth = GameManager.Player.GetComponent<ObjectHealth>();
        }
        else
        {
            //Show health as a fraction and as percent
            healthPercent.text = string.Format("Health: {0}/{1} : {2}%", playerHealth.Health, playerHealth.MaxHealth, playerHealth.HealthPercent().ToString());
        }
        //press escape to bring up popup panel
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            panel.SetActive(true);
            GameManager.PauseGame();
        }

        if (!panel.activeSelf && GameManager.Paused)
        {
            GameManager.UnpauseGame();
        }
    }

    
    /// <summary>
    /// To be called when clicking the Yes button in the panel that is shown when pressing esc key 
    /// </summary>
    public void ExitApplication()
    {
        Application.Quit();
    }
}
