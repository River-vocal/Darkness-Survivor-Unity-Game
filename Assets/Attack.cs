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
        gameObject.GetComponent<Renderer>().material.color = new Color(0, 0, 0, 0);
    }

    public void AttackRight() {
        weaponCollider.enabled = true;
        gameObject.GetComponent<Renderer>().material.color = new Color(0, 100, 100, 20);
    }

    public void StopAttack() {
        weaponCollider.enabled = false;
        gameObject.GetComponent<Renderer>().material.color = new Color(0, 0, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Enemy") {
            // Deal damage to the enemy
            Enemy enemy = other.GetComponent<Enemy>();
            // enemy.OnHit();
        }
    }

}
