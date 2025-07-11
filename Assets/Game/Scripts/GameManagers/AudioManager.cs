using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class AudioManager : MonoBehaviour
{
    [Inject]
    private DataLoader _dataLoader;
    private AudioSource _audioSource;

    private AudioClip _countDownAudio;
    private AudioClip _answerTimeAudio;
    private AudioClip _intenceAudio;
    private AudioClip _booAudio;
    private AudioClip _hoorayAudio;



    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _countDownAudio = _dataLoader.Audio[0];
        _answerTimeAudio = _dataLoader.Audio[1];
        _intenceAudio = _dataLoader.Audio[2];
        _booAudio = _dataLoader.Audio[3];
        _hoorayAudio = _dataLoader.Audio[4];
    }
    public void Play—ountDownAudio()
    {
        PlayClip(_countDownAudio, loop: false);
    }
    public void PlayAnswerTimeAudio()
    {
        PlayClip(_answerTimeAudio, loop: true);
    }
    public void PlayIntenceAudio()
    {
        PlayClip(_intenceAudio, loop: false);
    }
    public void PlayBooAudio()
    {
        PlayClip(_booAudio, loop: false);
    }
    public void PlayHoorayAudio()
    {
        PlayClip(_hoorayAudio, loop: false);
    }


    private void PlayClip(AudioClip clip, bool loop)
    {
        if (clip == null) return;

        _audioSource.clip = clip;
        _audioSource.loop = loop;
        _audioSource.Play();
    }

}
