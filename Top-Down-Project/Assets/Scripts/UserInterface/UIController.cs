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
    [SerializeField] HealthBar healthPrefab;
    [SerializeField] Transform enemyHealthHolder;
    Camera uiCamera;

    private void Awake()
    {
        uiCamera = GetComponent<Canvas>().worldCamera;
        if (Instance == null)
        {
            Instance = this;
        }
        
    }
    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
    /// connect the health bar on UI to the active player with this script
    /// </summary>
    /// <param name="player"></param>
    #region Helper Methods
    public void PlayerHealthBar(Player player)
    {
        healthBar.SetTarget(player.MyHealth);
    }

    public void EnemyHealthBar(Enemy enemy)
    {
        
        
        HealthBar enemyHealth = Instantiate(healthPrefab);
        enemyHealth.transform.SetParent(enemyHealthHolder.transform, false);
        enemyHealth.IsTracking = true;        
        enemyHealth.SetHealthPlacement(enemy.transform.Find("EnemyHealth"), uiCamera);
        enemyHealth.SetTarget(enemy.MyHealth);
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
