using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    [SerializeField] private GameObject wonMenu;
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Play reach finish point");
        GotoWonMenu();
    }
    
    void GotoWonMenu()
    {
        wonMenu.SetActive(true);
        Time.timeScale = 0f;
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
