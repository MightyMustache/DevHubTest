using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class GameOverTextAnimation : MonoBehaviour
{
    private TMP_Text _gameOverText;

    private void Awake()
    {
        _gameOverText = GetComponent<TMP_Text>();
    }

    private void Start()
    {
        AnimateGameOver();
    }

    public void AnimateGameOver()
    {
        _gameOverText.alpha = 0;
        _gameOverText.transform.localScale = Vector3.zero;

        Sequence seq = DOTween.Sequence();
        seq.Append(_gameOverText.DOFade(1f, 0.5f));
        seq.Join(_gameOverText.transform.DOScale(1f, 0.5f).SetEase(Ease.OutBack));
        seq.Append(_gameOverText.transform.DOScale(1.1f, 0.4f).SetLoops(-1, LoopType.Yoyo));
    }
}
