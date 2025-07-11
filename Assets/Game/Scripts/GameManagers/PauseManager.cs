using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PauseManager : MonoBehaviour
{
    public bool Pause { get; private set; }

    public static event Action<bool> OnPause;

    public void SetPause(bool pause)
    {
        OnPause.Invoke(pause);
        Pause = pause;
    }

    private void Start()
    {
        SetPause(true);
    }

    private void OnDisable()
    {
        OnPause = null;
    }
}
