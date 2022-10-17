using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LostMenu : MonoBehaviour
{
    private Energy energy;
    private GameObject lostMenu;

    private void Awake() {
        energy = GameObject.FindWithTag("Player").GetComponent<Energy>();
        energy.OnEmpty += energy_OnEmpty;
        lostMenu = transform.Find("lostMenu").gameObject;
        Time.timeScale = 1f;
    }

    private void energy_OnEmpty(object sender, System.EventArgs e)
    {
        GlobalAnalysis.state = "lose";
        AnalysisSender.Instance.postRequest("play_info", GlobalAnalysis.buildPlayInfoData());
        GlobalAnalysis.cleanData();
        lostMenu.SetActive(true);
        Time.timeScale = 0f;
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
