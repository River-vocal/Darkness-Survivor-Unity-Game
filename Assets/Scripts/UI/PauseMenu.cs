using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private VoidEventChannel gamePauseEventChannel;
    [SerializeField] private VoidEventChannel gameResumeEventChannel;

    // [SerializeField] protected AudioInfoBroadcaster audioInfoBroadcaster;
    public void Pause()
    {
        GetComponent<Canvas>().enabled = true;
        GetComponent<Animator>().enabled = true;
        
        Time.timeScale = 0f;
        gamePauseEventChannel.Broadcast();
    }

    public void Resume()
    {
        GetComponent<Canvas>().enabled = false;
        GetComponent<Animator>().enabled = false;
        
        Time.timeScale = 1f;
        gameResumeEventChannel.Broadcast();
    }

    public void GoLevelSelection()
    {
        Resume();
        SceneManager.LoadScene(0);
    }

}
