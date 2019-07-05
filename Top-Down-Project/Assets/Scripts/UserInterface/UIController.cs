using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class UIController : MonoBehaviour
{
    public static UIController Instance { get; private set; }
    
    [Header("Pause/End info")]
    [SerializeField] TextMeshProUGUI pauseEndText;

    [Header("Health Bar Settings")]
    [SerializeField] HealthBar healthBar;
    [SerializeField] HealthBar healthPrefab;
    [SerializeField] Transform enemyHealthHolder;
    Camera uiCamera;

    [Header("Weapon Info")]
    [SerializeField] Image weaponIcon;
    [SerializeField] Material weaponMaterial;

    [Header("Player Stats")]
    [SerializeField] TextMeshProUGUI lives;
    [SerializeField] string livesTextFormat;

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
            //GameManager.PauseGame();
        }

        //show weapon Icon
        if (GameManager.Player)
        {
            if (GameManager.Player.Gun)
            {
                weaponIcon.overrideSprite = GameManager.Player.Gun.Icon;
                weaponIcon.material = null;
            }
            else
            {
                weaponIcon.material = weaponMaterial;
            }
            

            lives.text = string.Format(livesTextFormat, GameManager.Instance.Lives);
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
        SceneManager.LoadScene("StartMenu");
    }

    public void Paused()
    {
        pauseEndText.text = "PAUSED";
    }

    public void GameOver()
    {
        pauseEndText.text = "GAME OVER!";
    }
}
