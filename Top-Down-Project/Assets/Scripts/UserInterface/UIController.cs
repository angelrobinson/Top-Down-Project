using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEditor;


public class UIController : MonoBehaviour
{
    public static UIController Instance { get; private set; }
    
    [Header("Pause/End info")]
    [SerializeField] TextMeshProUGUI pauseEndText = default;

    [Header("Game Timer info")]
    [SerializeField] TextMeshProUGUI timer = default;
    [SerializeField] string gameTimerTextFormat;

    [Header("Health Bar Settings")]
    [SerializeField] HealthBar healthBar;
    [SerializeField] HealthBar healthPrefab;
    [SerializeField] Transform enemyHealthHolder = default;
    Camera uiCamera;

    [Header("Weapon Info")]
    [SerializeField] Image weaponIcon = default;
    [SerializeField] Material weaponMaterial = default;

    [Header("Player Stats")]
    [SerializeField] TextMeshProUGUI lives = default;
    [SerializeField] string livesTextFormat;

    [Header("Score Info")]
    [SerializeField] TextMeshProUGUI score =default;
    [SerializeField] string scoreTextFormat;
    

    private void Awake()
    {
        //defaults
        uiCamera = GetComponent<Canvas>().worldCamera;
        if (Instance == null)
        {
            Instance = this;
        }

        if (string.IsNullOrEmpty(livesTextFormat))
        {
            livesTextFormat = "{0}";
        }

        if (string.IsNullOrEmpty(scoreTextFormat))
        {
            scoreTextFormat = "{0}";
        }

        if (string.IsNullOrEmpty(gameTimerTextFormat))
        {
            gameTimerTextFormat = "Time Left: {0}";
        }
    }
    private void Start()
    {
        if (healthBar == null)
        {
            healthBar = GetComponentInChildren<HealthBar>();
        }
        if (healthPrefab == null)
        {
            healthPrefab = (HealthBar)AssetDatabase.LoadAssetAtPath<HealthBar>("Assets/Prefabs/WorldUI/HealthBar");
        }
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

        score.text = string.Format(scoreTextFormat, GameManager.Instance.Score);

        timer.text = string.Format(gameTimerTextFormat, GameManager.Instance.TimeLeft);
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

    /// <summary>
    /// When game is paused and the Pause window is open update text to show correct text
    /// </summary>
    public void Paused()
    {
        pauseEndText.text = "PAUSED";
    }

    /// <summary>
    /// Show correct text when game is over
    /// </summary>
    public void GameOver()
    {
        pauseEndText.text = "GAME OVER!";
        
    }
}
