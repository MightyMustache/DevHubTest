using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using TMPro;

public class ContentManager : MonoBehaviour
{
   
    private TMP_Text _bannerText;
    private SpriteRenderer _bannerContent;
    private TMP_Text _rightSignText;
    private TMP_Text _leftSignText;
    private string _currentContent;

    [Inject]
    private DataLoader _dataLoader;

    public bool IsRightCorrect { get; private set; }

    [Inject(Id = "Banner")]
    private GameObject _banner;

    [Inject(Id = "LeftSign")]
    private GameObject _leftSign;

    [Inject(Id = "RightSign")]
    private GameObject _rightSign;


    void Start()
    {
        _bannerText = _banner.GetComponentInChildren<TMP_Text>();
        _bannerContent =_banner.GetComponentInChildren<SpriteRenderer>();
        _rightSignText = _rightSign.GetComponentInChildren<TMP_Text>();
        _leftSignText = _leftSign.GetComponentInChildren<TMP_Text>();
    }

    private void ChooseContentRandom()
    {
        string ContentName = _dataLoader.CorrectNames[Random.Range(0, _dataLoader.CorrectNames.Count)];
        if (_currentContent != ContentName)
            _currentContent = ContentName;
        else
            ChooseContentRandom();
    }

    public void PutContent()
    {
        ChooseContentRandom();
        string IncorrectName = _dataLoader.IncorrectNames[_currentContent];

        _bannerText.text = "What is this?";
        _bannerContent.sprite = _dataLoader.CorrectPairs[_currentContent]; 

        IsRightCorrect = Random.value > 0.5f;

        _rightSignText.text = IsRightCorrect ? _currentContent : IncorrectName;
        _leftSignText.text = IsRightCorrect ? IncorrectName : _currentContent;
    }

    public void PutAnswerContent(bool icontype)
    {
        _bannerContent.sprite = _dataLoader.AnswerIcons[icontype];
    }
    public void ClearContent()
    {
        _bannerText.text = string.Empty;
        _bannerContent.sprite = null;
        _rightSignText.text = string.Empty;
        _leftSignText.text = string.Empty;
    }
  
}
