using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    public float damage = 2;
    public Collider2D swordCollider;
    Vector2 rightAttackOffset;
    public void Start() {
        swordCollider = GetComponent<Collider2D>();
        rightAttackOffset = transform.position;
    }
    // Start is called before the first frame update
    public void AttackRight() {
        swordCollider.enabled = true;
        transform.localPosition = rightAttackOffset;
    }

    public void AttackLeft() {
        swordCollider.enabled = true;
        transform.localPosition = new Vector2(-rightAttackOffset.x, rightAttackOffset.y);
    }

    public void StopAttack() {
        swordCollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Enemy") {
            var enemy = other.GetComponent<Enemy>();
            if (enemy != null) {
                enemy.Health -= damage;
            }
        }
    }
}
