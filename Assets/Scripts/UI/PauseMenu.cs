using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private VoidEventChannel gamePauseEventChannel;
    [SerializeField] private VoidEventChannel gameResumeEventChannel;

    private void OnEnable()
    {
        gamePauseEventChannel.AddListener(Pause);
        gameResumeEventChannel.AddListener(Resume);
    }

    private void OnDisable()
    {
        gamePauseEventChannel.RemoveListener(Pause);
        gameResumeEventChannel.RemoveListener(Resume);
    }

    public void Pause()
    {
        GetComponent<Canvas>().enabled = true;
        GetComponent<Animator>().enabled = true;
        
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        GetComponent<Canvas>().enabled = false;
        GetComponent<Animator>().enabled = false;
        
        Time.timeScale = 1f;
    }

    public void GoLevelSelection()
    {
        Resume();
        SceneManager.LoadScene(0);
    }

}
