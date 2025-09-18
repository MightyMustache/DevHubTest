using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DataLoader
{
    public Dictionary<string, Sprite> CorrectPairs { get; private set; }
    public Dictionary<string, string> IncorrectNames { get; private set; }

    public List<string> CorrectNames { get; private set; }
    public Dictionary<bool, Sprite> AnswerIcons { get; private set; }

    public GameObject AIPrefab { get; private set; }

    public AudioClip[] Audio { get; private set; }

    public DataLoader()
    {
        Load();
    }

    public void Load()
    {
        Sprite[] sprites = Resources.LoadAll<Sprite>("MemeIcons");
        Sprite[] answericons = Resources.LoadAll<Sprite>("Icons");
        Audio = Resources.LoadAll<AudioClip>("Audio");

        AIPrefab = Resources.Load<GameObject>("AIPrefab");


        string[] correctNames = Resources
            .Load<TextAsset>("TextData/CorrectNames")
            .text
            .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

        string[] incorrectNames = Resources
            .Load<TextAsset>("TextData/IncorrectNames")
            .text
            .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);



        CorrectNames = new List<string>(correctNames);
        CorrectPairs = new Dictionary<string, Sprite>();
        IncorrectNames = new Dictionary<string, string>();
        AnswerIcons = new Dictionary<bool, Sprite> { { false, answericons[0] }, {true, answericons[1]} };


        for (int i = 0; i < sprites.Length; i++)
        {
            CorrectPairs.Add(correctNames[i], sprites[i]);
            IncorrectNames.Add(correctNames[i], incorrectNames[i]);
        }
    }
}
