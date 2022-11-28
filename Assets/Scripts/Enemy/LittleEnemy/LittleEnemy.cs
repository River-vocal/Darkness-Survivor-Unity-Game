using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LittleEnemy : MonoBehaviour
{
    
    private bool isLittleEnemyDeath = false;
    private bool isLittleEnemyBeAttcked = false;
    [SerializeField] public int damage = 20;
    [SerializeField] public float speed = 5f;
    
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
    
    private void Deactivate()
    {
        
        gameObject.SetActive(false);
    }
    
    // private void OnTriggerEnter2D(Collider2D col)
    // {
    //     if (col.gameObject.CompareTag("Player"))
    //     {
    //         if (col.GetComponent<Energy>().CurEnergy < damage) {
    //             GlobalAnalysis.player_status = "smallenemy_dead";
    //             Debug.Log("lose by: small enemy");
    //         }
    //         GlobalAnalysis.smallenemy_damage += damage;
    //         col.GetComponent<Player>().TakeDamage(damage);
    //     }
    // }
}
