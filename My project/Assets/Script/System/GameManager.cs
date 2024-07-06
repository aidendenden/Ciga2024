using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool isPause;
    
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
    }
    
    
}
