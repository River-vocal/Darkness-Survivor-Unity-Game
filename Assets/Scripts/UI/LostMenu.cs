using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LostMenu : MonoBehaviour
{
    [SerializeField] VoidEventChannel playerDefeatedEventChannel;

    private void OnEnable()
    {
        playerDefeatedEventChannel.AddListener(Defeat);
    }

    private void OnDisable()
    {
        playerDefeatedEventChannel.RemoveListener(Defeat);
    }

    private void Defeat()
    {
        GetComponent<Canvas>().enabled = true;
        GetComponent<Animator>().enabled = true;
        
        Time.timeScale = 0f;
    }
    

    public void GoLevelSelection()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void Respawn()
    {
        Time.timeScale = 1f;
        Respawn respawn = GameObject.FindGameObjectWithTag("Player").GetComponent<Respawn>();
        respawn.Remake();

        HideUI();
    }

    private void HideUI()
    {
        GetComponent<Canvas>().enabled = false;
        GetComponent<Animator>().enabled = false;
    }
}
