using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ButtonsController : MonoBehaviour
{

    public Canvas pauseCanvas;
    public Canvas settingsCanvas;
    public GameObject resetMenu;
    public UnityEngine.UI.Slider soundFill;

    public void PauseButton()
    {
        pauseCanvas.gameObject.SetActive(true);
        Time.timeScale = 0;
        AudioSource[] audios = FindObjectsOfType<AudioSource>();
        foreach (AudioSource a in audios)
            a.Pause();
    }

    public void ResumeButton()
    {
        pauseCanvas.gameObject.SetActive(false);
        Time.timeScale = 1;
        AudioSource[] audios = FindObjectsOfType<AudioSource>();
        foreach (AudioSource a in audios)
            a.Play();
    }

    public void StartLevel()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void GoToShop()
    {
        SceneManager.LoadScene("StoreMenu");
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void Settings()
    {
        settingsCanvas.gameObject.SetActive(true);
    }
    public void OnSoundChange()
    {
        PlayerPrefs.SetFloat("Sound", soundFill.value);
    }
    public void StartArcade()
    {
        PlayerPrefs.SetInt("Arcade", 1);
        SceneManager.LoadScene("SampleScene");
    }
    public void ReturnToMainMenu()
    {
        settingsCanvas.gameObject.SetActive(false);
    }
    public void ResetButton()
    {
        resetMenu.SetActive(true);
    }
    public void ResetCommit()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void ResetCancel()
    {
        resetMenu.SetActive(false);
    }
}

