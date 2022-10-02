using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceBullets : MonoBehaviour {

    public float speed = 10f;
    public int damage = 40;
    public Rigidbody2D rb;
    public GameObject impactEffect;

    // Use this for initialization
    void Start () {
        rb.velocity = (-1) * transform.up * speed;
    }

    void OnTriggerEnter2D (Collider2D hitInfo)
    {
        PlayerController player = hitInfo.GetComponent<PlayerController>();
        if (player != null)
        {
            // player receive one bullet from the sky
            player.IncreaseBullet();
        }

        Instantiate(impactEffect, transform.position, transform.rotation);

        Destroy(gameObject);
    }
	
}