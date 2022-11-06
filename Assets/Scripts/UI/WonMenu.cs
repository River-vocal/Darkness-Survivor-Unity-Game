using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WonMenu : MonoBehaviour
{
    [SerializeField] private VoidEventChannel levelClearedEventChannel;
    [SerializeField] private StringEventChannel clearTimeTextEventChannel;
    [SerializeField] private TMP_Text timeText;

    private void OnEnable()
    {
        levelClearedEventChannel.AddListener(Win);
        clearTimeTextEventChannel.AddListener(SetTimeText);
    }

    private void OnDisable()
    {
        levelClearedEventChannel.RemoveListener(Win);
        clearTimeTextEventChannel.RemoveListener(SetTimeText);
    }

    private void SetTimeText(string obj)
    {
        timeText.text = obj;
    }

    private void Win()
    {
        if (SceneManager.GetActiveScene().buildIndex >= SceneManager.sceneCountInBuildSettings - 1) return;
        GetComponent<Canvas>().enabled = true;
        GetComponent<Animator>().enabled = true;

        Time.timeScale = 0;
    }

    public void GoNextLevel()
    {
        Time.timeScale = 1f;

        int nextLevelIndex = SceneManager.GetActiveScene().buildIndex;
        if (nextLevelIndex < 5)
        {
            nextLevelIndex += 1;
        }

        SceneManager.LoadScene(nextLevelIndex);
    }
}