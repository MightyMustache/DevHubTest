using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Zenject;
public class StageManager : MonoBehaviour
{

    [SerializeField] private float _preparationDuration = 3f;
    [SerializeField] private float _answeringDuration = 10f;
    [SerializeField] private float _preResultDuration = 3f;
    [SerializeField] private float _resultDuration = 3f;
    [SerializeField] private int _AmountAiToSpawn = 10;


    private PlayerMovement _playerMovement;
    private AnswerValidator _answerValidator;
    private ContentManager _contentManager;
    private AIManager _aiManager;
    private AudioManager _audioManager;
    private TMP_Text _timerValue;
    private TMP_Text _scoreValue;
    private int _score;
    private GameStage _currentStage = GameStage.None;


    [Inject(Id = "GameOverMenu")]
    private GameObject _gameOverMenu;

    [Inject(Id = "Timer")]
    private GameObject _timerGO;

    [Inject(Id = "Score")]
    private GameObject _scoreGO;

    [Inject(Id = "Result")]
    private GameObject _resultGO;

    [Inject]
    public void Constract(ContentManager contentManager, AnswerValidator answerValidator, PlayerMovement playerMovement, AIManager aIManager, AudioManager audioManager)
    {
        _contentManager = contentManager;
        _answerValidator = answerValidator;
        _playerMovement = playerMovement;
        _aiManager = aIManager;
        _audioManager = audioManager;
    }

    private void Start()
    {
        _timerValue = _timerGO.GetComponent<TMP_Text>();
        _scoreValue = _scoreGO.GetComponent<TMP_Text>();
    }

    public GameStage CurrentStage => _currentStage;


    public void StartGame()
    {
        _aiManager.AISpawn(_AmountAiToSpawn);
        StartCoroutine(RunStages());
    }

    private IEnumerator RunStages()
    {
        yield return StartCoroutine(RunStage(GameStage.Preparation, _preparationDuration));
        yield return StartCoroutine(RunStage(GameStage.Answering, _answeringDuration));
        yield return StartCoroutine(RunStage(GameStage.PreResult, _preResultDuration));
        yield return StartCoroutine(RunStage(GameStage.Result, _resultDuration));

        TriggerStageEvents(CurrentStage);
    }

    private IEnumerator RunStage(GameStage stage, float duration)
    {
        SetStage(stage);

        TriggerStageEvents(stage);

        float remainingTime = duration;

        while (remainingTime > 0)
        {
            _timerValue.text = ((int)remainingTime).ToString();
            yield return new WaitForSeconds(1f);
            remainingTime -= 1f;
        }
        _timerValue.text = "0";
        yield return new WaitForSeconds(1f);
    }

    private void SetStage(GameStage stage)
    {
        _currentStage = stage;
    }

    private void ResetLoop()
    {
        SetStage(GameStage.None);
        _playerMovement.ResetPosition();
        _playerMovement.DisableMovement(false);
        _timerGO.SetActive(true);
        _aiManager.AIAllSceneClear();
        _contentManager.ClearContent();
        StartGame();
    }

    private void TriggerStageEvents(GameStage stage)
    {
        switch (stage)
        {
            case GameStage.Preparation:
                _audioManager.Play—ountDownAudio();
                break;

            case GameStage.Answering:
                _audioManager.PlayAnswerTimeAudio();
                _aiManager.AIMoveInPosition();
                _contentManager.PutContent();
                break;

            case GameStage.PreResult:
                _audioManager.PlayIntenceAudio();
                _playerMovement.DisableMovement(true);
                _timerValue.gameObject.SetActive(false);
                break;

            case GameStage.Result:
                _contentManager.ClearContent();
                bool result = _answerValidator.CheckPlayerAnswer();
                _contentManager.PutAnswerContent(result);
                if (result)
                {
                    _audioManager.PlayHoorayAudio();
                    _score++;
                    _scoreValue.text = $"Score: {_score}";
                }
                else
                {
                    _audioManager.PlayBooAudio();
                }

                _AmountAiToSpawn = _answerValidator.CheckAIAnswer();
                SetStage(_answerValidator.PlayerAnswer ? GameStage.Reset : GameStage.GameOver);
                break;

            case GameStage.GameOver:
                _resultGO.GetComponent<TMP_Text>().text = $"Your score: {_score}";
                _gameOverMenu.SetActive(true);
                break;

            case GameStage.Reset:
               
                ResetLoop();
                break;
        }
    }
    public enum GameStage
    {
        None,
        Preparation,
        Answering,
        PreResult,
        Result,
        GameOver,
        Reset
    }
}
