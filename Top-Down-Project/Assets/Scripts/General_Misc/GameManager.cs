﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Singleton script for Main game management
/// </summary>
public class GameManager : MonoBehaviour
{
    //unity events
    [SerializeField] private UnityEvent OnPause;
    [SerializeField] private UnityEvent OnResume;
    [SerializeField] private UnityEvent OnEndGame;


    //static variables
    public static GameManager Instance { get; private set; }
    public static Player Player { get; private set; }
    public static bool Paused { get; private set; }

    //normal variables
    [Header("Player Settings")]
    [SerializeField] GameObject playerPrefab;
    [SerializeField] List<Transform> playerSpawns;
    [SerializeField] float respawnTime;
    [SerializeField] int maxLives;
    int currentLives;
    bool respawning;

    //Properties
    public int Lives { get { return currentLives; } private set { currentLives = value; } }
    

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

        //set initial lives
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

    }


    #region Helper Methods
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
    #endregion
}
