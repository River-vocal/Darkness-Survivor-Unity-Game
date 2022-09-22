using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Animator animator;
    public int health = 5;
    public static int KilledEnemy = 0;
    [SerializeField] private HealthBar healthBar;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        healthBar.setHealth(health);
        

        if (health <= 0){
            animator.SetTrigger("Defeated");
            healthBar.SetActive(false);
            KilledEnemy ++;
        }else{
            animator.SetTrigger("Damaged");
        }

    }

    // public void TakeDamage() {
    //     animator.SetTrigger("Damaged");
    // }
    public void RemoveEnemy() {
        Destroy(gameObject);
    }
}
