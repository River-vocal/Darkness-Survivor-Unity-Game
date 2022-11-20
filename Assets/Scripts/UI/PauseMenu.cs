using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private VoidEventChannel gamePauseEventChannel;
    [SerializeField] private VoidEventChannel gameResumeEventChannel;

    private Canvas canvas;
    private Animator animator;

    public static bool GamePaused;
    private static readonly int Replay = Animator.StringToHash("Replay");

    private void Awake()
    {
        canvas = GetComponent<Canvas>();
        animator = GetComponent<Animator>();
    }

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
        canvas.enabled = true;
        animator.enabled = true;
        animator.Play("PauseMenu", -1, 0);

        Time.timeScale = 0f;
    }

    public void Resume()
    {
        GamePaused = false;

        canvas.enabled = false;
        animator.enabled = false;

        Time.timeScale = 1f;
    }

    public void GoLevelSelection()
    {
        Resume();
        SceneManager.LoadScene(0);
    }
}