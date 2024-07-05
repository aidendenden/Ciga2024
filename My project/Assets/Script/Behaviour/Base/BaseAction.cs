using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAction
{
    void Execute();
}

public abstract class BaseAction : MonoBehaviour, IAction
{
    public abstract void Execute();
}