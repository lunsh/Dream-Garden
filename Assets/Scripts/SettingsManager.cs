using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsManager : MonoBehaviour
{
    private static SettingsManager _instance;

    public static SettingsManager Instance
    {
        get
        {
            return _instance;
        }
    }


    public float soundVolume = 0.5f;
    public float musicVolume = 0.5f;
    [SerializeField] private AudioSource soundSource;
    [SerializeField] private BGManager bgManager;
    private AudioSource bgSource;

    public bool transitioning;

    private void Awake()
    {
        if ( _instance == null )
        {
            _instance = this;
            DontDestroyOnLoad(_instance);
        }
        else
        {
            Destroy(this.gameObject);
        }
        transitioning = false;
        bgSource = bgManager.GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        bgSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        soundSource.volume = soundVolume;
        if (transitioning == false)
        {
            bgSource.volume = musicVolume;
        } else if (transitioning == true && sceneName == "Game" )
        {
            bgManager.ChangeSong();
            bgManager.playlist = true;
            StartCoroutine(FadeAudioSource.StartFade(bgSource, 1f, musicVolume));
            transitioning = false;
        }
    }
}
