using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip _menuAudio;
    [SerializeField] private AudioClip _gameAudio;
    [SerializeField] private AudioClip _loseAudio;

    private Scene _curScene;
    private AudioSource _audioSource;
    
    private void Awake()
    {
        SetUpSingleton();
        _curScene = SceneManager.GetActiveScene();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        ChangeSong();
    }

    private void ChangeSong()
    {
        var scene = SceneManager.GetActiveScene();
        
        if (scene == _curScene)
            return;

        var sceneNumber = scene.buildIndex;

        switch(sceneNumber)
        {
            case 0:
            {
                _audioSource.clip = _menuAudio;
                _audioSource.Play();
                break;
            }
            case 1:
            {
                _audioSource.clip = _gameAudio;
                _audioSource.Play();
                break;
            }
            case 2:
            {
                _audioSource.clip = _loseAudio;
                _audioSource.Play();
                break;
            }
        }

        _curScene = scene;
    }
    
    private void SetUpSingleton()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    
    
    

}
