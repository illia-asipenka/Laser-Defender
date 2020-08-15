using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayerOld : MonoBehaviour
{
    static MusicPlayerOld instance = null;
    public AudioClip startClip;
    public AudioClip gameClip;
    public AudioClip endClip;

    private AudioSource music;



    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            Debug.Log("Duplicate music player is destroyed!");
        }
        else
        {
            instance = this;
            GameObject.DontDestroyOnLoad(gameObject);
            music = GetComponent<AudioSource>();
            music.clip = startClip;
            music.loop = true;
            music.Play();
        }
    }

    private void OnLevelWasLoaded(int level)
    {
        Debug.Log("MusicPlayer : loaded level " + level);
        music.Stop();
        if(level == 0)
        {
            music.clip = startClip;
        }
        if (level == 1)
        {
            music.clip = gameClip;
        }
        if (level == 2)
        {
            music.clip = endClip;
        }

        music.loop = true;
        music.Play();
    }


}
