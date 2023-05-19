using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ButtonsController : MonoBehaviour
{

    public Canvas pauseCanvas;

    public void PauseButton()
    {
        pauseCanvas.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void ResumeButton()
    {
        pauseCanvas.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void StartLevel()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void GoToShop()
    {
        SceneManager.LoadScene("Shop");
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void Settings()
    {

    }
    public void StartArcade()
    {
        PlayerPrefs.SetInt("Arcade", 1);
        SceneManager.LoadScene("SampleScene");
    }
}
