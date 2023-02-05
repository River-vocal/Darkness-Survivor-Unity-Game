using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float speed = 20f;
    public int damage = 40;
    public Rigidbody2D rb;
    public GameObject impactEffect;

    // Use this for initialization
    void Start () {
        rb.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D (Collider2D hitInfo)
    {
        Health health = hitInfo.GetComponent<Health>();
        if(health != null) health.CurHealth -= damage;

        Instantiate(impactEffect, transform.position, transform.rotation);

        Destroy(gameObject);
    }
	
}