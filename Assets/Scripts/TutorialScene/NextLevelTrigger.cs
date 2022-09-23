using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelTrigger : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if(Enemy.KilledEnemy == 2){
            SceneManager.LoadScene(2);
        }
    }
}
