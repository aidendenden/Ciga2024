using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Restart : MonoBehaviour
{
    private void Start()
    {
        GameManager.Instance.ReStartGame();
    }
}