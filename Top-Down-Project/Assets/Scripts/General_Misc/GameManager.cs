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
    public static AudioSource AudioSource { get; private set; }


    //normal variables
    [Header("SFX")]
    [SerializeField] AudioClip backgroundMusic;
    [SerializeField] AudioClip menuMusic;

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
    [SerializeField] List<PlayerScore> scores = new List<PlayerScore>();

    //Properties
    public int Lives { get { return currentLives; } private set { currentLives = value; } }
    public int Score { get; private set; }

    public List<PlayerScore> Scores { get { return scores; } }
    

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
        AudioSource = GetComponent<AudioSource>();

        if (AudioSource)
        {
            if (backgroundMusic)
            {
                AudioSource.clip = backgroundMusic;
            }

            AudioSource.Play();
        }

        
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
            playerPrefab = Resources.Load("Prefabs/Player.prefab") as GameObject;
        }

        if (gameTime == 0)
        {
            gameTime = 3;
        }

        minutes = gameTime;
        
        currentLives = maxLives;

        GetScores();
    }

    // Update is called once per frame
    void Update()
    {
        if (Paused)
        {
            return;
        }
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
    public void Reset()
    {
        Awake();
    }

    private void GetScores()
    {
        for (int i = 0; i < 5; i++)
        {
            AddScore(PlayerPrefs.GetString("player[" + i + "]"), PlayerPrefs.GetInt("score[" + i + "]"));
        }


    }

    public void SaveScores()
    {
        scores.Sort(delegate (PlayerScore score1, PlayerScore score2) { return score2.playerScore - score1.playerScore; });
        
        //go through sorted list and save top five
        for (int i = 0; i < 5; i++)
        {
            PlayerPrefs.SetString("player[" + i + "]", scores[i].playerName);
            PlayerPrefs.SetInt("score[" + i + "]", scores[i].playerScore);
        }
    }

    public void AddScore(PlayerScore ps)
    {
        scores.Add(ps);
    }

    public void AddScore(string pName, int score)
    {
        PlayerScore temp = new PlayerScore { playerName = pName, playerScore = score };
        scores.Add(temp);
    }
    /// <summary>
    /// Timer count down in Minutes:Seconds:Miliseconds
    /// </summary>
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
            //Paused = true;
            PauseGame();
            OnEndGame.Invoke();
        }
    }

    /// <summary>
    /// check for current lives.  If there are lives left, respawn, otherwise end game
    /// </summary>
    private void HandleDeath()
    {
        Lives--;
        if (currentLives > 0)
        {
            respawning = true;
            Invoke("SpawnPlayer", respawnTime);
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
    [System.Serializable]
    public struct PlayerScore
    {
        public string playerName;
        public int playerScore;
    }
    #endregion
}
