﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    // This is a basic level manager. It persists between scene loads as written.
    // Should be added to an empty game object in the first scene.
    // Can be accessed globally via LevelManager.Instance.x();

    public static LevelManager Instance { set; get; }

    private PlayerController player;
    public int lives;
    private int currentLevel;
    private string[] levels;
    public bool gameStarted;
    private bool gameover;
    private bool levelReady;
    public int playerLevel;

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        Instance = this;
        currentLevel = 0;
        gameStarted = false;
        lives = 100;
        levelReady = false;
        playerLevel = 0;
    }

    void Start()
    {
        levels = new string[] { "start", "prologue", "Level1" };
        SceneManager.sceneLoaded += OnSceneLoaded;

    }
	
    // Update is called once per frame
    void Update()
    {
        // Start menu
        if (!gameStarted)
        {
            if (Input.GetKey("return"))
            {
                GameObject.Find("Main Camera").GetComponent<AudioSource>().Stop();
                GameObject.Find("menu-startsound").GetComponent<AudioSource>().Play();
                gameStarted = true;
                Invoke("NextLevel", .25f);
            }
        }

        // In game, level has been loaded
        else if (levelReady == true)
        {
            {
                if (!player.alive)
                {
                    lives--;
                    // If you run out of lives, you lose
                    if (lives == 0 && !gameover)
                    {
                        SceneManager.LoadScene("Gameover");
                        gameover = true;

                    }
                    else
                    {
                        RestartLevel();
                    }                
                }
            }
        }
    }
        
    // Call this whenever a level is beaten to move to the next level
    public void NextLevel()
    {
        // Update the level that the player should start at in the next game level
        playerLevel = player.getLevel();

        levelReady = false;
        currentLevel++;
        // If you've beaten the last level, you win.
        if (currentLevel == levels.Length)
        {
            SceneManager.LoadScene("Youwin");
        }
        else
        {
            SceneManager.LoadScene(levels[currentLevel]);
        }
    }

    public void RestartLevel()
    {
        levelReady = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // We may want to provide some functionality to reset the game (e.g. reset number of lives and go back to level 1)
    public void ResetGame()
    {
        currentLevel = 0;
    }


    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != "Start")
        {
            player = GameObject.Find("Player").GetComponent<PlayerController>();

            // Reset the player state
            player.setLevel(playerLevel);
            levelReady = true;
        }
    }

    public void updateCurrentLevel()
    {
        currentLevel = SceneManager.GetActiveScene().buildIndex;
    }
}
