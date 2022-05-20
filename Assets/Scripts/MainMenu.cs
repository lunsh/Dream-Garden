using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject creditsMenu;

    public Animator transition;
    public Animator musicTransition;
    public float transitionTime = 1f;

    public void PlayGame()
    {
        SFXManager.sfxInstance.Audio.PlayOneShot(SFXManager.sfxInstance.NewGame);
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        // play animation
        transition.SetTrigger("Start");
        musicTransition.SetTrigger("FadeOut");
        // wait
        yield return new WaitForSeconds(transitionTime);
        // load scene
        SceneManager.LoadScene(levelIndex);
    }

    public void SettingsMenu()
    {
        settingsMenu.SetActive(true);
        SFXManager.sfxInstance.Audio.PlayOneShot(SFXManager.sfxInstance.Click);
    }

    public void CreditsMenu()
    {
        creditsMenu.SetActive(true);
        SFXManager.sfxInstance.Audio.PlayOneShot(SFXManager.sfxInstance.Click);
    }

    public void closeMenus()
    {
        settingsMenu.SetActive(false);
        creditsMenu.SetActive(false);
        SFXManager.sfxInstance.Audio.PlayOneShot(SFXManager.sfxInstance.Close);
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
        SFXManager.sfxInstance.Audio.PlayOneShot(SFXManager.sfxInstance.Click);
    }
}