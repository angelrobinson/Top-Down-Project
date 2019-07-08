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
    [SerializeField] TMP_InputField nameField;

    [Header("Score Info")]
    [SerializeField] TextMeshProUGUI score =default;
    [SerializeField] string scoreTextFormat;
    public GameObject scoreBoard;
    public PlayerScoreBoard[] scores;

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

        scoreBoard = transform.Find("ScoresPanel").gameObject;
        scores = GetComponentsInChildren<PlayerScoreBoard>();
        scoreBoard.SetActive(false);
    }
    private void Start()
    {
        if (healthBar == null)
        {
            healthBar = GetComponentInChildren<HealthBar>();
        }
        if (healthPrefab == null)
        {
            healthPrefab = Resources.Load("Prefabs/WorldUI/HealthBar") as HealthBar;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Paused)
        {
            return;
        }
        
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
            

            
        }


        lives.text = string.Format(livesTextFormat, GameManager.Instance.Lives);

        score.text = string.Format(scoreTextFormat, GameManager.Instance.Score);

        timer.text = string.Format(gameTimerTextFormat, GameManager.Instance.TimeLeft);
    }



    #region Helper Methods
    /// <summary>
    /// connect the health bar on UI to the active player with this script
    /// </summary>
    /// <param name="player"></param>
    public void PlayerHealthBar(Player player)
    {
        healthBar.SetTarget(player.MyHealth);
    }

    /// <summary>
    /// Connect the Enemy health bar with the enemy being spawned
    /// </summary>
    /// <param name="enemy"></param>
    public void EnemyHealthBar(Enemy enemy)
    {
        HealthBar enemyHealth = Instantiate(healthPrefab);
        enemyHealth.transform.SetParent(enemyHealthHolder.transform, false);
        enemyHealth.IsTracking = true;        
        enemyHealth.SetHealthPlacement(enemy.transform.Find("EnemyHealth"), uiCamera);
        enemyHealth.SetTarget(enemy.MyHealth);
    }
    
    /// <summary>
    /// To be called when clicking the Yes button in the panel that is shown when pressing esc key 
    /// </summary>
    public void ExitApplication()
    {
        GameManager.Instance.SaveScores();
        Application.Quit();
        //TODO: return to this when I figure out how to properly reset the GamaManager
        //SceneManager.LoadScene("StartMenu");


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

        lives.text = string.Format(livesTextFormat, GameManager.Instance.Lives);

        for (int i = 1; i < 6; i++)
        {
            scores[i].nameText.text = GameManager.Instance.Scores[i-1].playerName;
            scores[i].scoreText.text = GameManager.Instance.Scores[i-1].playerScore.ToString();
        }

        
    }

    public void GetCurrentPlayerInfo()
    {
        scores[0].nameText.text = nameField.text;
        scores[0].scoreText.text = score.text;
        GameManager.Instance.AddScore(nameField.text, GameManager.Instance.Score);

    }

    #endregion
}
