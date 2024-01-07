using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TempleRun_SceneLoader : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("TempleRunLevel1");
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("TempleRunOpening");
    }

    public void QuitGame()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
