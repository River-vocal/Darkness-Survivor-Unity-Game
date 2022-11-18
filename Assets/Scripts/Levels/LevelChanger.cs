using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    public Animator animator;
    private GameObject gbToLoad;
    private GameObject gbToDisappear;
    private int levelToLoad;
    void Update()
    {
        
    }

    public void SetToLoadeMenuGB(GameObject toLoad)
    {
        gbToLoad = toLoad;
    }
    
    public void SetToDisappearMenuGB(GameObject toDisappear)
    {
        gbToDisappear = toDisappear;
        animator.SetTrigger("MenuFadeOut");
    }
    
    
    
    public void OnFade(int LevelToLoad)
    {
        animator.SetTrigger("FadeOut");
        levelToLoad = LevelToLoad;
        
    }

    public void MenuOnFadeComplete()
    {
        gbToDisappear.SetActive(false);
        gbToLoad.SetActive(true);
        animator.SetTrigger("FadeIn");
        
    }
    
    public void LevelOnFadeComplete()
    {
        SceneManager.LoadScene(levelToLoad);
    }
}
