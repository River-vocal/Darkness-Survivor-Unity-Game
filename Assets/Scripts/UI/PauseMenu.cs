using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    
    [SerializeField] protected AudioInfoBroadcaster audioInfoBroadcaster;
    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        audioInfoBroadcaster.PauseMusic();
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        audioInfoBroadcaster.ResumeMusic();
    }

    public void Replay()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoNextLevel()
    {
        Time.timeScale = 1f;

        int nextLevelIndex = SceneManager.GetActiveScene().buildIndex;
        if (nextLevelIndex < 5)
        {
            nextLevelIndex += 1;
        }

        SceneManager.LoadScene(nextLevelIndex);
    }

    public void GoLevelSelectionLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

}
