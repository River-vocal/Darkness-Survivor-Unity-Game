using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletController : MonoBehaviour
{
    public float speed = 15f;
    public int playerBulletDamage = 15;
    public int DamagePopup = 20;
    public Rigidbody2D rb;
    public GameObject impactEffect;
    // [SerializeField] protected LongRangeAttack longRangeAttack;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    // void Update()
    // {
        
    // }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        // Enemy layer == 11
        
        if (hitInfo.name == "Wall" || hitInfo.name == "Ground" || hitInfo.name == "Obstacle" || hitInfo.gameObject.layer == 11)
        {
            Debug.Log("hitInfo.name: " + hitInfo.name);
            Instantiate(impactEffect, transform.position, transform.rotation);
            speed = 0;
            
            Destroy(gameObject);

            if (hitInfo.CompareTag("Boss"))
            {
                //particle effects
                // if (hitInfo.GetComponent<Boss>() != null)
                // {
                    // Player.VisualEffectSystemManager.GenerateBleedParticleEffect(hitInfo.GetComponent<Boss>().transform);
                // }
                Health health = hitInfo.GetComponent<Health>();
                if (health) health.CurHealth -= playerBulletDamage;

            } else if (hitInfo.gameObject.layer == 11)
            {
                // longRangeAttack.playerBulletCount += 1;
                Destroy(hitInfo.gameObject);
            }


            //Animator bulletAnimator = gameObject.GetComponent<Animator>();
            //bulletAnimator.SetBool("IsHit", true);
            //Invoke("DestroyBullet", 0.5f);

        }

        // gameObject.SetActive(false);
        
        
    }

    void DestroyBullet()
    {
        Destroy(gameObject);
    }


}
