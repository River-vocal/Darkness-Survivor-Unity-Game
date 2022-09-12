using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    Collider2D weaponCollider;
    
    public float damage = 3;

    private void Start() {
        weaponCollider = GetComponent<Collider2D>();
        weaponCollider.enabled = false;
    }

    public void AttackRight() {
        weaponCollider.enabled = true;
    }

    public void StopAttack() {
        weaponCollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Enemy") {
            // Deal damage to the enemy
            Enemy enemy = other.GetComponent<Enemy>();
            enemy.OnHit();
        }
    }

}
