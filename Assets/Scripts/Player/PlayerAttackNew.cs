using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerAttackNew : MonoBehaviour
{

    public int attackDamage;


    public Vector3 attackOffset;   // Adjust and set offset in Unity
    // It could avoid boss damaged when play stands on the head of the boss. 
    public float attackRange = 0.8f;
    public LayerMask attackMask;
    public PlayerControllerNew player;
    private Animator animator;

    // Animation Event
    public void Attack()
    {
        animator = GetComponent<Animator>();
        animator.ResetTrigger(Attack1);
        // player.LockMovement();
        attackDamage = player.attackDamage;

        Vector3 pos = transform.position;

        // Make sure the attack circle is in front of the player

        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;
        // if heavy beats detected
        // Player can only fire bullet start from the second tutorial
        

        // normal attack
        Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
        if (colInfo != null)
        {
            if (colInfo.tag == "Drop")
            {
                colInfo.GetComponent<EnemyDrops>().dropDeath();
            }
            else
            {
                TriggerScreenShake();

                colInfo.GetComponent<Health>().CurHealth -= attackDamage;
            }


            Debug.Log("Player Attack");
        }
    }
    // Animation Event
    public void EndAttack()
    {
        // player.UnlockMovement();
    }


    // help to see the attack circle range
    // The white circle of the object
    void OnDrawGizmosSelected()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;
        Gizmos.DrawWireSphere(pos, attackRange);
    }

    private CinemachineImpulseSource cinemachineImpulseSource;
    private static readonly int Attack1 = Animator.StringToHash("attack");

    private void Start()
    {
        cinemachineImpulseSource = GetComponent<CinemachineImpulseSource>();
    }

    private void TriggerScreenShake()
    {
        cinemachineImpulseSource.GenerateImpulse();
    }

}