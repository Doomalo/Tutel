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
    public GameObject[] pauseObjects;
    public GameObject[] deathObjects;

    public void PauseButton()
    {
        pauseCanvas.gameObject.SetActive(true);
        foreach (GameObject go in pauseObjects)
            go.SetActive(true);
        Time.timeScale = 0;
        AudioSource[] audios = FindObjectsOfType<AudioSource>();
        foreach (AudioSource a in audios)
            a.Pause();
    }

    public void ResumeButton()
    {
        pauseCanvas.gameObject.SetActive(false);
        foreach (GameObject go in pauseObjects)
            go.SetActive(false);
        Time.timeScale = 1;
        AudioSource[] audios = FindObjectsOfType<AudioSource>();
        foreach (AudioSource a in audios)
            a.Play();
    }

    public void StartLevel()
    {
        Time.timeScale = 1;
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
        pauseCanvas.gameObject.SetActive(false);
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
        pauseCanvas.gameObject.SetActive(true);
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
    public void Defeat()
    {
        pauseCanvas.gameObject.SetActive(true);
        foreach (GameObject go in deathObjects)
            go.SetActive(true);
        float score = PlayerPrefs.GetFloat("Score");
        float highScore = PlayerPrefs.GetFloat("HighScore", 0.0f);
        if (highScore < score)
        {
            highScore = score;
            PlayerPrefs.SetFloat("HighScore", highScore);
        }
        deathObjects[2].GetComponent<UnityEngine.UI.Text>().text = score.ToString();
        deathObjects[4].GetComponent<UnityEngine.UI.Text>().text = highScore.ToString();
        Time.timeScale = 0;
        AudioSource[] audios = FindObjectsOfType<AudioSource>();
        foreach (AudioSource a in audios)
            a.Pause();
    }
}

