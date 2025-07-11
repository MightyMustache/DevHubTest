using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


public class MenuStartButton : MonoBehaviour
{
    private PauseManager _pauseManager;
    private StageManager _stageManager;


    [Inject(Id = "HUD")]
    private GameObject _HUD;

    [Inject(Id = "Menu")]
    private GameObject _menu;

    [Inject]
    public void Constract(PauseManager pauseManager, StageManager stageManager)
    {
        _pauseManager = pauseManager;
        _stageManager = stageManager;
    }

    public void GameStart()
    {
        _menu.SetActive(false);
        _pauseManager.SetPause(false);
        _HUD.SetActive(true);
        _stageManager.StartGame();
    }
}
