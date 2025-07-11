using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using TMPro;

public class AnswerValidator 
{
    [Inject(Id ="RightArea")]
    private GameObject _rightArea;

    [Inject(Id = "LeftArea")]
    private GameObject _leftArea;


    private GameObject _player;
    private ContentManager _contentManager;
    private AIManager _aiManager;

    public bool PlayerAnswer { get; private set; }

    [Inject]
    public void Constract(ContentManager ContentManager, PlayerMovement player, AIManager aIManager)
    {
        _contentManager = ContentManager;
        _player = player.gameObject;
        _aiManager = aIManager;
    }

    public bool CheckPlayerAnswer()
    {
        GameObject correctCircle = _contentManager.IsRightCorrect ? _rightArea : _leftArea;
        return PlayerAnswer = correctCircle.GetComponent<SphereCollider>().bounds.Contains(_player.transform.position);
    }

    public int CheckAIAnswer()
    {
        GameObject correctCircleGO = _contentManager.IsRightCorrect ? _rightArea : _leftArea;
        SphereCollider correctCircle = correctCircleGO.GetComponent<SphereCollider>();

        int count = 0;
        foreach (var obj in _aiManager.AIOnscene)
        {
            if (correctCircle.bounds.Contains(obj.transform.position))
                count++;
        }
        return count;
    }
}
