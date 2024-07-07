using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool isPause=false;
    public bool getKey=false;

    public LevelLoader levelLoader;
    
    void SingletonInit()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;
    }

    private void Start()
    {
        levelLoader = transform.GetComponent<LevelLoader>();
        isPause=false;
        getKey=false;
    }

    private void Awake()
    {
        SingletonInit();
    }

    public void REstart()
    {
        GameEventManager.Instance.ClearEventListeners();
        getKey=false;
        levelLoader.ReloadLevel();
    }

    public void NextLevel()
    {
        levelLoader.LoadNextLevel();
    }

    public void ReStartGame()
    {
        levelLoader.ReStart();
    }
    
}
