using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIController : MonoBehaviour
{
    public static UIController Instance { get; private set; }
    
    [SerializeField] GameObject panel;

    [Header("Health Bar Settings")]
    [SerializeField] HealthBar healthBar;
    [SerializeField] GameObject healthPrefab;

    //ObjectHealth playerHealth;
    


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        
    }
    private void Start()
    {
        //find the Player and get it's health component
        //playerHealth = GameManager.Player.GetComponent<ObjectHealth>();
    }
    // Update is called once per frame
    void Update()
    {
        //if (GameManager.Player && !playerHealth)
        //{
        //    playerHealth = GameManager.Player.GetComponent<ObjectHealth>();
        //}
        //else
        //{
        //    //Show health as a fraction and as percent
        //    //healthPercent.text = string.Format(healthTextFormat, playerHealth.Health, playerHealth.MaxHealth, playerHealth.HealthPercent().ToString());
        //}
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

    #region Helper Methods
    public void PlayerHealthBar(Player player)
    {
        healthBar.SetTarget(player.MyHealth);
    }

    public void EnemyHealthBar(Enemy enemy)
    {

    }
    #endregion
    /// <summary>
    /// To be called when clicking the Yes button in the panel that is shown when pressing esc key 
    /// </summary>
    public void ExitApplication()
    {
        Application.Quit();
    }
}
