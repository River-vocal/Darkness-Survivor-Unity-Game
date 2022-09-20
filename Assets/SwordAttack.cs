using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    public int damage = 10;
    public Collider2D swordCollider;
    Vector2 swordHitBoxOffset;
    Vector2 swordColliderOffset;
    public void Start() {
        swordHitBoxOffset = transform.localPosition;
        swordColliderOffset = swordCollider.offset;
    }
    // Start is called before the first frame update
    public void AttackRight() {
        transform.localPosition = swordHitBoxOffset;
        swordCollider.offset = swordColliderOffset;
        swordCollider.enabled = true;
    }

    public void AttackLeft() {
        transform.localPosition = new Vector2(-swordHitBoxOffset.x, swordHitBoxOffset.y);
        swordCollider.offset = new Vector2(-swordColliderOffset.x, swordColliderOffset.y);
        swordCollider.enabled = true;
    }

    public void StopAttack() {
        swordCollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("collision detected");
        if (other.tag == "Enemy") {
            var enemy = other.GetComponent<Boss>();
            if (enemy != null) {
                enemy.TakeDamage(damage);
            }
        }
    }
}
