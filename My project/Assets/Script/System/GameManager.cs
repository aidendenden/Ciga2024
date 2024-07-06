using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool isPause=false;
    public bool getKey=false;
        
    void SingletonInit()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;
    }
    
    private void Awake()
    {
        SingletonInit();
    }

    public void REstart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        GameEventManager.Instance.ClearEventListeners();
        getKey=false;
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
}
