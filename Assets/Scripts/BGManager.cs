using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGManager : MonoBehaviour
{

    public static BGManager bgInstance;
    private AudioSource _audiosource;
    public AudioClip[] songs;
    public int songPlaying;
    public bool playlist;
    [SerializeField] private float trackTimer;

    private void Awake()
    {
        if (bgInstance != null && bgInstance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        bgInstance = this;
        songPlaying = 0;
        playlist = false;
        DontDestroyOnLoad(bgInstance);
    }

    private void Start()
    {
        _audiosource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (playlist == true )
        {
            if ( _audiosource.isPlaying )
            {
                trackTimer += 1 * Time.deltaTime;
            }
            if ( ! _audiosource.isPlaying || trackTimer >= _audiosource.clip.length)
            {
                songPlaying++;
                if (songPlaying >= songs.Length)
                {
                    songPlaying = 0;
                }
                ChangeSong();
            }
        }
    }
    public void ChangeSong()
    {
        trackTimer = 0;
        _audiosource.clip = songs[songPlaying];
        _audiosource.Play();
    }
}
