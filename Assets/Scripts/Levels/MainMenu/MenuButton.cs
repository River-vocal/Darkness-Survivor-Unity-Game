using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
	public void PlayGame()
	{
		SceneManager.LoadScene(1);
	}
	
	

    
    public void QuitGame()
    {
	    Debug.Log("Quit Game!");
	    Application.Quit();         // Game will not quit in Unity Editor
    }
}
