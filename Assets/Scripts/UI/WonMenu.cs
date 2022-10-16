using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WonMenu : MonoBehaviour
{
    private Exit exit;
    private GameObject wonMenu;
    private void Awake() {
        exit = GameObject.Find("Exit").GetComponent<Exit>();
        wonMenu = transform.Find("wonMenu").gameObject;
        exit.OnPlayerReachExit += exit_OnPlayerReachExit;
    }

    private void exit_OnPlayerReachExit(object sender, System.EventArgs e){
        wonMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void GoNextLevel()
    {
        Time.timeScale = 1f;

        int nextLevelIndex = SceneManager.GetActiveScene().buildIndex;
        if (nextLevelIndex < 4)
        {
            nextLevelIndex += 1;
        }

        SceneManager.LoadScene(nextLevelIndex);
    }

    public void Replay()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
