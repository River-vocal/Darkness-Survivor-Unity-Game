using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LostMenu : MonoBehaviour
{
    private Energy energy;
    private GameObject lostMenu;

    private void Start()
    {
        energy = GameObject.FindWithTag("Player").GetComponent<Energy>();
        energy.OnEmpty += energy_OnEmpty;
        lostMenu = transform.Find("lostMenu").gameObject;
    }

    private void energy_OnEmpty(object sender, System.EventArgs e)
    {
        lostMenu.SetActive(true);
        Time.timeScale = 1f;
    }

    public void GoLevelSelectionLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void Replay()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
