using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsConfig : MonoBehaviour
{
    [SerializeField] private SettingsManager settings;
    [SerializeField] private GameObject deleteConfirmPanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject deleteButton;
    [SerializeField] private GameObject quitButton;
    [SerializeField] private GameObject quitHelpText;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider bgSlider;

    private Scene currentScene;

    private void Awake()
    {
        if ( settings == null )
        {
            settings = GameObject.FindWithTag("SettingsManager").GetComponent<SettingsManager>();
            sfxSlider.value = settings.soundVolume;
            bgSlider.value = settings.musicVolume;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        currentScene = SceneManager.GetActiveScene();
    }

    // Update is called once per frame
    void Update()
    {
        string sceneName = currentScene.name;
        if ( sceneName == "Game" )
        {
            deleteButton.SetActive(false); // can't delete your save game while playing, sorry
            quitButton.SetActive(true);
            quitHelpText.SetActive(true);

        } else
        {
            quitButton.SetActive(false);
            quitHelpText.SetActive(false);
            if (!PlayerPrefs.HasKey("saveData")) // no save data
            {
                deleteButton.SetActive(false);
            }
            else
            {
                deleteButton.SetActive(true);
            }
        }
    }

    public void DeleteSaveFile()
    {
        deleteConfirmPanel.SetActive(true);
    }

    public void CancelDelete()
    {
        deleteConfirmPanel.SetActive(false);
    }

    public void ConfirmDelete()
    {
        PlayerPrefs.DeleteAll();
        deleteConfirmPanel.SetActive(false);
    }

    public void onSFXValueChange(float newValue)
    {
        settings.soundVolume = newValue;
    }
    public void onBGValueChange(float newValue)
    {
        settings.musicVolume = newValue;
    }

    public void closeSettings()
    {
        settingsPanel.SetActive(false);
    }

    public void quitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
