using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private GameObject _HUD;
    [SerializeField] private GameObject _menu;
    [SerializeField] private GameObject _timer;
    [SerializeField] private GameObject _score;
    [SerializeField] private GameObject _result;
    [SerializeField] private GameObject _gameOverMenu;
    [SerializeField] private GameObject _banner;
    [SerializeField] private GameObject _rightSign;
    [SerializeField] private GameObject _leftSign;
    [SerializeField] private GameObject _rightArea;
    [SerializeField] private GameObject _leftArea;
    [SerializeField] private GameObject _midArea;





    public override void InstallBindings()
    {
        Container.Bind<PauseManager>()
       .FromComponentInHierarchy()
       .AsSingle();

        Container.Bind<StageManager>()
       .FromComponentInHierarchy()
       .AsSingle();

        Container.Bind<ContentManager>()
       .FromComponentInHierarchy()
       .AsSingle();

        Container.Bind<AudioManager>()
       .FromComponentInHierarchy()
       .AsSingle();

        Container.Bind<PlayerMovement>()
       .FromComponentInHierarchy()
       .AsSingle();

        Container.Bind<AIPool>()
       .FromComponentInHierarchy()
       .AsSingle();

        Container.Bind<AIManager>()
       .FromComponentInHierarchy()
       .AsSingle();

        Container.Bind<AnswerValidator>()
       .AsSingle();

        Container.Bind<DataLoader>()
       .AsSingle();

        Container.Bind<GameObject>()
       .WithId("HUD")
       .FromInstance(_HUD);

        Container.Bind<GameObject>()
       .WithId("Menu")
       .FromInstance(_menu);

        Container.Bind<GameObject>()
       .WithId("Timer")
       .FromInstance(_timer);

        Container.Bind<GameObject>()
       .WithId("Score")
       .FromInstance(_score);

        Container.Bind<GameObject>()
       .WithId("Result")
       .FromInstance(_result);

        Container.Bind<GameObject>()
       .WithId("GameOverMenu")
       .FromInstance(_gameOverMenu);

        Container.Bind<GameObject>()
       .WithId("Banner")
       .FromInstance(_banner);

        Container.Bind<GameObject>()
       .WithId("RightSign")
       .FromInstance(_rightSign);

        Container.Bind<GameObject>()
       .WithId("LeftSign")
       .FromInstance(_leftSign);

        Container.Bind<GameObject>()
       .WithId("RightArea")
       .FromInstance(_rightArea);

        Container.Bind<GameObject>()
       .WithId("LeftArea")
       .FromInstance(_leftArea);

        Container.Bind<GameObject>()
       .WithId("MidArea")
       .FromInstance(_midArea);
    }
}
