using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LittleEnemy : MonoBehaviour
{
    
    private bool isLittleEnemyDeath = false;
    private bool isLittleEnemyBeAttcked = false;
    
    

    // Update is called once per frame

    public void LittleEnemyBeAttacked()
    {
        isLittleEnemyBeAttcked = true;
    }

    public bool GetDeathStatus()
    {
        return isLittleEnemyDeath;
    }
    
    public bool GetBeAttackedStatus()
    {
        return isLittleEnemyBeAttcked;
    }
    
    public void SetDeathStatus(bool status)
    {
        isLittleEnemyDeath = status;
    }
    
    public void SetBeAttackedStatus(bool status)
    {
        isLittleEnemyBeAttcked = status;
    }
}
