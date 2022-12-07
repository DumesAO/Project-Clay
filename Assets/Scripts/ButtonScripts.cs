using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScripts : MonoBehaviour
{
    public Canvas pauseMenu;
    public void LoadLevel(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void StartLevel(string name)
    {
        Time.timeScale = 1;
        PauseManager.IsPaused = false;
        LoadLevel(name);
    }
    public void Pause() {
        pauseMenu.enabled = true;
        Time.timeScale = 0;
        PauseManager.IsPaused=true;
    }
    public void UnPause() {
        Time.timeScale = 1;
        pauseMenu.enabled = false;
        PauseManager.IsPaused = false;
    }

}
