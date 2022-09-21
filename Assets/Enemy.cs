using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Animator animator;
    public float health = 5;
    public float Health {
        set {
            health = value;
            TakeDamage();
            if (health <= 0) Defeated();
        }
        get {
            return health;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage() {
        animator.SetTrigger("Damaged");
    }
    public void Defeated() {
        animator.SetTrigger("Defeated");
    }
    public void RemoveEnemy() {
        Destroy(gameObject);
    }
}
