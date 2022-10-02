using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    
    public int attackDamage;
    
    
    public Vector3 attackOffset;   // Adjust and set offset in Unity
    // It could avoid boss damaged when play stands on the head of the boss. 
    public float attackRange = 2f;
    public LayerMask attackMask;
    public PlayerController player;
    
    

    // Animation Event
    public void Attack()
    {
        
        player.LockMovement();
        attackDamage = player.attackDamage;
        Vector3 pos = transform.position;
        
        // Make sure the attack circle is in front of the player
        if (player.isFacingRight)
        {
            pos += transform.right * attackOffset.x;
        }
        else
        {
            pos -= transform.right * attackOffset.x;
        }
        pos += transform.up * attackOffset.y;

        Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
        if (colInfo != null)
        {
            if (colInfo.name == "Boss")
            {
                colInfo.GetComponent<Boss>().TakeDamage(attackDamage);
            }

            if (colInfo.name == "Nature_props_01")
            {
                colInfo.GetComponent<EnemyWood>().TakeDamage(attackDamage);
            }
            Debug.Log("Player Attack");
        }
    }
    // Animation Event
    public void EndAttack()
    {
        player.UnlockMovement();
    }
    
    
    // help to see the attack circle range
    // The white circle of the object
    void OnDrawGizmosSelected()
    {
        Vector3 pos = transform.position;
        if (player.isFacingRight)
        {
            pos += transform.right * attackOffset.x;
        }
        else
        {
            pos -= transform.right * attackOffset.x;
        }
        
        pos += transform.up * attackOffset.y;

        Gizmos.DrawWireSphere(pos, attackRange);
    }
}