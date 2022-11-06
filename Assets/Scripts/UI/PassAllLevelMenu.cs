using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PassAllLevelMenu : MonoBehaviour
{
    [SerializeField] public VoidEventChannel playerWinEventChannel;
    [SerializeField] public StringEventChannel clearTimeTextEventChannel;
    [SerializeField] private TMP_Text timeText;

    private void OnEnable()
    {
        playerWinEventChannel.AddListener(Win);
        clearTimeTextEventChannel.AddListener(SetTimeText);
    }

    private void OnDisable()
    {
        playerWinEventChannel.RemoveListener(Win);
        clearTimeTextEventChannel.RemoveListener(SetTimeText);
    }

    private void Win()
    {
        if (SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCountInBuildSettings - 1) return;
        
        GetComponent<Canvas>().enabled = true;
        GetComponent<Animator>().enabled = true;

        Time.timeScale = 0;
    }

    public void GoCongratulationScene()
    {
        // Work in progress?
        // Currently go to level selection scene
        GoLevelSelection();
    }

    private void SetTimeText(string obj)
    {
        timeText.text = obj;
    }
    
    public void GoLevelSelection()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}