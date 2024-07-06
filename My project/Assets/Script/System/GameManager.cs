using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool isPause=false;
    public bool getKey=false;

    public LevelLoader LevelLoader;
    
    void SingletonInit()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;
    }

    private void Start()
    {
        LevelLoader = transform.GetComponent<LevelLoader>();
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
        LevelLoader.ReloadLevel();
    }

    public void NextLevel()
    {
        LevelLoader.LoadNextLevel();
    }
    
}
