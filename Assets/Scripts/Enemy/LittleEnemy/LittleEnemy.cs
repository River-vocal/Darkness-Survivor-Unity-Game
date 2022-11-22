using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LittleEnemy : MonoBehaviour
{
    
    private bool isLittleEnemyDeath = false;
    

    // Update is called once per frame

    public void LittleEnemyDeath()
    {
        isLittleEnemyDeath = true;
    }

    public bool GetDeathStatus()
    {
        return isLittleEnemyDeath;
    }
    
    public void SetDeathStatus(bool status)
    {
        isLittleEnemyDeath = status;
    }
}
