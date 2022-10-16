using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private GameObject pauseMenu;
    public static bool GameIsPaused = false;

    private void Awake() {
        pauseMenu = transform.Find("pauseMenu").gameObject;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    // [SerializeField] protected AudioInfoBroadcaster audioInfoBroadcaster;
    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        // audioInfoBroadcaster.PauseMusic();
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        // audioInfoBroadcaster.ResumeMusic();
    }

    public void Replay()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoLevelSelectionLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

}
