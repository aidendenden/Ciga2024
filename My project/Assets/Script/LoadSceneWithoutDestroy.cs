using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSceneWithoutDestroy : MonoBehaviour
{
    public static LoadSceneWithoutDestroy Instance;
    public int count;
    private static bool isInstanceCreated = false;

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
        if (!isInstanceCreated)
        {
            DontDestroyOnLoad(gameObject);
            isInstanceCreated = true;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    // LoadSceneWithoutDestroy.Instance.count++;
}
