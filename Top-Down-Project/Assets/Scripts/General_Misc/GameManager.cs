using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor;

/// <summary>
/// Singleton script for Main game management
/// </summary>
public class GameManager : MonoBehaviour
{
    //unity events
    [SerializeField] private UnityEvent OnPause;
    [SerializeField] private UnityEvent OnResume;
    [SerializeField] private UnityEvent OnEndGame ;


    //static variables
    public static GameManager Instance { get; private set; }
    public static Player Player { get; private set; }
    public static bool Paused { get; private set; }

    //normal variables
    [Header("Player Settings")]
    [SerializeField] GameObject playerPrefab;
    [SerializeField] List<Transform> playerSpawns = default;
    [SerializeField] float respawnTime;
    [SerializeField] int maxLives;
    int currentLives;
    bool respawning;

    [Header("Game Timer")]
    [Tooltip("Total time In Minutes")]
    [SerializeField] int gameTime;
    int minutes;
    int seconds;
    float miliseconds; //in seconds


    [Header("Player Prefs Info")]
    [SerializeField] List<string> scores;

    //Properties
    public int Lives { get { return currentLives; } private set { currentLives = value; } }
    public int Score { get; private set; }

    public string TimeLeft
    {
        get
        {
            return minutes + ":" + seconds;
        }        
    }
    

    private void Awake()
    {
        Instance = this;
        Player = FindObjectOfType<Player>();

        //set defaults 
        if (respawnTime == 0)
        {
            respawnTime = 10;
        }
        
        if (maxLives == 0)
        {
            maxLives = 3;
        }

        if (playerPrefab == null)
        {
            playerPrefab = (GameObject) AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Player.prefab", typeof(GameObject));
        }

        if (gameTime == 0)
        {
            gameTime = 3;
        }

        minutes = gameTime;
        
        currentLives = maxLives;
    }

    // Update is called once per frame
    void Update()
    { 
        //if player is dead and has been deleted
        if (!Player && !respawning)
        {
            HandleDeath();
        }

        //press escape to bring up popup panel
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }


        Timer();


    }


    #region Helper Methods
    private void Timer()
    {
        if (miliseconds <= 0)
        {
            if (seconds <= 0)
            {
                minutes--;
                seconds = 59;
            }
            else if (seconds >= 0)
            {
                seconds--;
            }

            miliseconds = 100;
        }

        miliseconds -= Time.deltaTime * 100;

        if (minutes == 0 && seconds == 0)
        {
            PauseGame();
            OnEndGame.Invoke();
        }
    }
    private void HandleDeath()
    {
        //check for current lives.  If there are lives left, respawn, otherwise end game
        if (currentLives > 0)
        {
            respawning = true;
            Invoke("SpawnPlayer", respawnTime);
            Lives--;
        }
        else
        {
            PauseGame();
            OnEndGame.Invoke();
        }
    }

    /// <summary>
    /// Spawn Player at a random location based on Spawn Points set up
    /// </summary>
    private void SpawnPlayer()
    {
        int index = Random.Range(0, playerSpawns.Count);
        Player = Instantiate(playerPrefab, playerSpawns[index].position, playerSpawns[index].rotation).GetComponent<Player>();
        respawning = false;
        
    }

    /// <summary>
    /// Pause the game using Time.timeScale
    /// </summary>
    public void PauseGame()
    {
        OnPause.Invoke();
        Paused = true;
        Time.timeScale = 0f;
    }

    /// <summary>
    /// Unpause the game by reseting Time.timescale
    /// </summary>
    public void UnpauseGame()
    {
        OnResume.Invoke();
        Paused = false;
        Time.timeScale = 1f;
    }

    /// <summary>
    /// Update the player score
    /// </summary>
    /// <param name="amt"></param>
    public void UpdateScore(int amt = 1)
    {
        Score += amt;
    }
    #endregion

    #region Embeded Classes and Structs
    public struct PlayerScore
    {
        public string name;
        public int score;
    }
    #endregion
}
