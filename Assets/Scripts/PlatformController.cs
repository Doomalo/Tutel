using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlatformController : MonoBehaviour
{
    public void LoadPlatform(int input)
    {
        DataController.platformNum = input;
        SceneManager.LoadScene(1);
    }
}
